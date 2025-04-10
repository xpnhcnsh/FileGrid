using System;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Entities;

public class FileGridContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<ResourcePermission> ResourcePermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Share> Shares { get; set; }

    public FileGridContext(DbContextOptions<FileGridContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<ResourcePermission>()
            .HasKey(rp => rp.Id);

        modelBuilder.Entity<ResourcePermission>()
            .HasOne(rp => rp.Resource)
            .WithMany(r => r.ResourcePermissions)
            .HasForeignKey(rp => rp.ResourceId);

        modelBuilder.Entity<ResourcePermission>()
            .HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.RoleId);

        modelBuilder.Entity<ResourcePermission>()
            .HasOne(rp => rp.User)
            .WithMany()
            .HasForeignKey(rp => rp.UserId);

        modelBuilder.Entity<Share>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Share>()
            .HasMany(s => s.TargetUsers)
            .WithMany(u => u.Shares)
            .UsingEntity(j => j.ToTable("ShareUsers"));

        modelBuilder.Entity<Resource>()
            .HasMany(r => r.Shares)
            .WithMany(s => s.Resources)
            .UsingEntity<Dictionary<string, object>>(
                "ResourceShare",
                j => j.HasOne<Share>().WithMany().HasForeignKey("ShareId"),
                j => j.HasOne<Resource>().WithMany().HasForeignKey("ResourceId"),
                j =>
                {
                    j.HasKey("ResourceId", "ShareId");
                });
    }
}
