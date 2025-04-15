using System;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Entities;

public class FileGridContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Share> Shares { get; set; }

    public FileGridContext(DbContextOptions<FileGridContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region User
        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(x => x.Id);

            // User -> Resource 的一对多
            u.HasMany(x => x.UploadedResources)
             .WithOne(x => x.Creator)
             .HasForeignKey(x => x.CreatedById)
             .OnDelete(DeleteBehavior.NoAction);

            // User -> Share 的一对多（分享者）
            u.HasMany(x => x.Shares)
             .WithOne(x => x.OwnerUser)
             .HasForeignKey(x => x.OwnerUserId)
             .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region Share
        modelBuilder.Entity<Share>(s =>
        {
            s.HasKey(x => x.Id);

            // Share -> User 的多对多（目标用户）
            s.HasMany(x => x.TargetUsers)
             .WithMany(x => x.SharedWithMe)
             .UsingEntity<Dictionary<string, object>>(
                 "ShareTargetUser",
                 j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.NoAction),
                 j => j.HasOne<Share>().WithMany().HasForeignKey("ShareId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasKey("ShareId", "UserId")
             );

            // Share -> Resource 的多对多（使用隐式连接表）
            s.HasMany(x => x.Resources)
             .WithMany(x => x.Shares)
             .UsingEntity<Dictionary<string, object>>(
                 "ShareResource",
                 j => j.HasOne<Resource>().WithMany().HasForeignKey("ResourceId").OnDelete(DeleteBehavior.NoAction),
                 j => j.HasOne<Share>().WithMany().HasForeignKey("ShareId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasKey("ShareId", "ResourceId")
             );
        });
        #endregion

        #region Resource
        modelBuilder.Entity<Resource>(r =>
        {
            r.HasKey(x => x.Id);
            r.HasIndex(x => new { x.BucketName, x.Path }).IsUnique();
        });
        #endregion

        #region Role
        modelBuilder.Entity<Role>(r =>
        {
            r.HasKey(x => x.Id);

            // Role -> User 的多对多
            r.HasMany(x => x.Users)
             .WithMany(x => x.Roles)
             .UsingEntity<Dictionary<string, object>>(
                 "UserRole",
                 j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasKey("UserId", "RoleId")
             );
        });
        #endregion

        #region Permission
        modelBuilder.Entity<Permission>(p =>
        {
            p.HasKey(x => x.Id);

            // Permission -> User 的多对多
            p.HasMany(x => x.Users)
             .WithMany(x => x.Permissions)
             .UsingEntity<Dictionary<string, object>>(
                 "UserPermission",
                 j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasOne<Permission>().WithMany().HasForeignKey("PermissionId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasKey("UserId", "PermissionId")
             );
        });
        #endregion
    }
}
