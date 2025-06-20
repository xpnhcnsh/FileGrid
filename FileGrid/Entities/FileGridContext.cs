using FileGrid.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Entities
{
    public class FileGridContext : DbContext
    {
        public FileGridContext(DbContextOptions<FileGridContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        public DbSet<ProjectOutsource> ProjectOutsources { get; set; }
        public DbSet<ProjectPartA> ProjectPartAs { get; set; }
        public DbSet<ProjectResource> ProjectResources { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourcePermission> ResourcePermissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<ShareResource> ShareResources { get; set; }
        public DbSet<ShareUser> ShareUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroupPermissionPolicy> UserGroupPermissionPolicies { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }
        public DbSet<UserProjectGroup> UserProjectGroups { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<InvitationCode> InvitationCodes { get; set; }
        public DbSet<DrillHole> DrillHoles { get; set; }
        public DbSet<Phase> Phases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Company
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Type).IsRequired().HasConversion<string>();
                entity.Property(e => e.Address);
                entity.Property(e => e.Description);
                entity.Property(e => e.TaxCode);

                entity.HasOne(e => e.ContactUser)
                    .WithMany()
                    .HasForeignKey(e => e.ContactUserId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.Departments)
                    .WithOne(d => d.Company)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Department
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.CompanyId).IsRequired();
                entity.Property(e => e.IsProjectGroup)
                        .IsRequired()
                        .HasDefaultValue(false); // 默认不是项目组  

                entity.HasOne(e => e.Company)
                    .WithMany(c => c.Departments)
                    .HasForeignKey(e => e.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Projects)
                    .WithOne(p => p.ProjectGroup)
                    .HasForeignKey(p => p.ProjectGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 用户所属部门关系
                entity.HasMany(d => d.UserDepartments)
                    .WithOne(ud => ud.Department)
                    .HasForeignKey(ud => ud.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 用户可访问项目组关系
                entity.HasMany(d => d.AccessibleUsers)
                    .WithOne(upg => upg.ProjectGroup)
                    .HasForeignKey(upg => upg.ProjectGroupId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Permission
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PermissionName).IsRequired();
            });

            // Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.ProjectGroupId).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasConversion<string>();
                entity.Property(e => e.PlannedStartDate);
                entity.Property(e => e.ActualStartDate);
                entity.Property(e => e.PlannedEndDate);
                entity.Property(e => e.ActualEndDate);

                entity.HasOne(e => e.ProjectGroup)
                    .WithMany()
                    .HasForeignKey(e => e.ProjectGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Manager)
                    .WithMany()
                    .HasForeignKey(e => e.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DeputyManager)
                    .WithMany()
                    .HasForeignKey(e => e.DeputyManagerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SafetyLeader)
                    .WithMany()
                    .HasForeignKey(e => e.SafetyLeaderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ProductionLeader)
                    .WithMany()
                    .HasForeignKey(e => e.ProductionLeaderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TechnicalLeader)
                    .WithMany()
                    .HasForeignKey(e => e.TechnicalLeaderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PartA)
                    .WithMany()
                    .HasForeignKey(e => e.PartAId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.OtherParticipants)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("ProjectParticipants"));
            });

            // ProjectFile
            modelBuilder.Entity<ProjectFile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RelativePath).IsRequired();
                entity.Property(e => e.FileName).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.FileSize).IsRequired();
                entity.Property(e => e.ProjectId).IsRequired();
                entity.Property(e => e.ProjectResourceId).IsRequired();

                entity.HasOne(e => e.ProjectResource)
                    .WithMany(p => p.Files)
                    .HasForeignKey(e => e.ProjectResourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // ProjectOutsource (Many-to-Many relationship between Project and Company)
            modelBuilder.Entity<ProjectOutsource>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.OutsourceId });

                entity.HasOne(e => e.Project)
                    .WithMany(p => p.Outsources)
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Outsource)
                    .WithMany(c => c.ProjectsOutSources)
                    .HasForeignKey(e => e.OutsourceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ProjectPartA (Many-to-Many relationship between Project and Company for PartA)
            modelBuilder.Entity<ProjectPartA>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.PartAId });

                entity.HasOne(e => e.Project)
                    .WithMany()
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PartA)
                    .WithMany(c => c.ProjectPartAs)
                    .HasForeignKey(e => e.PartAId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ProjectResource
            modelBuilder.Entity<ProjectResource>(entity =>
            {
                entity.HasKey(e => e.ProjectId);
                entity.Property(e => e.MinioBucket).IsRequired();
                entity.Property(e => e.BasePath).IsRequired();

                entity.HasOne(e => e.Project)
                    .WithOne(p => p.Resource)
                    .HasForeignKey<ProjectResource>(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Resource
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BucketName).IsRequired();
                entity.Property(e => e.FullPath).IsRequired();
                entity.Property(e => e.ResourceType).IsRequired().HasConversion<string>();
                entity.Property(e => e.Version).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.Property(e => e.FileType).IsRequired();
                entity.Property(e => e.FileStatus).IsRequired().HasConversion<string>();
                entity.Property(e => e.CreatedById).IsRequired();

                entity.HasOne(e => e.Creator)
                    .WithMany(u => u.UploadedResources)
                    .HasForeignKey(e => e.CreatedById)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configure the relationship between Resource and ShareResource
                entity.HasMany<ShareResource>()
                    .WithOne(sr => sr.Resource)
                    .HasForeignKey(sr => sr.ResourceId);
            });

            // ResourcePermission
            modelBuilder.Entity<ResourcePermission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PrincipalType).IsRequired().HasConversion<string>();
                entity.Property(e => e.PrincipalTag).IsRequired();
                entity.Property(e => e.ResourceType).IsRequired().HasConversion<string>();
                entity.Property(e => e.ResourcePath).IsRequired();
                entity.Property(e => e.Operations).IsRequired().HasConversion<string>();
                entity.Property(e => e.ApplyToSubItems).IsRequired();
                entity.Property(e => e.GrantTime).IsRequired();

                entity.HasOne(e => e.GrantedBy)
                    .WithMany()
                    .HasForeignKey(e => e.GrantedById)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RoleName).IsRequired();
            });

            // Share
            modelBuilder.Entity<Share>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OwnerId).IsRequired();
                entity.Property(e => e.ShareType).IsRequired().HasConversion<string>();
                entity.Property(e => e.ShareLink);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.ExpiresAt);

                entity.HasOne(e => e.Owner)
                    .WithMany(u => u.Shares)
                    .HasForeignKey(e => e.OwnerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ShareResource (Many-to-Many relationship between Share and Resource)
            modelBuilder.Entity<ShareResource>(entity =>
            {
                entity.HasKey(e => new { e.ShareId, e.ResourceId });

                entity.HasOne(e => e.Share)
                    .WithMany(s => s.ShareResources)
                    .HasForeignKey(e => e.ShareId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Resource)
                    .WithMany()
                    .HasForeignKey(e => e.ResourceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ShareUser (Many-to-Many relationship between Share and User)
            modelBuilder.Entity<ShareUser>(entity =>
            {
                entity.HasKey(e => new { e.ShareId, e.UserId });

                entity.HasOne(e => e.Share)
                    .WithMany(s => s.ShareUsers)
                    .HasForeignKey(e => e.ShareId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.SharedWithMe)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.UserGroup).IsRequired().HasConversion<string>();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.UpdatedAt).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.CompanyId);
                entity.Property(e => e.DepartmentId);
                entity.Property(e => e.JobTitle);
                entity.Property(e => e.PhoneNumber);

                entity.HasOne(e => e.Company)
                    .WithMany(c => c.Users)
                    .HasForeignKey(e => e.CompanyId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.Department)
                    .WithMany()
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 用户所属部门关系
                entity.HasMany(u => u.UserDepartments)
                    .WithOne(ud => ud.User)
                    .HasForeignKey(ud => ud.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 用户可访问项目组关系
                entity.HasMany(u => u.AccessibleProjectGroups)
                    .WithOne(upg => upg.User)
                    .HasForeignKey(upg => upg.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserGroupPermissionPolicy
            modelBuilder.Entity<UserGroupPermissionPolicy>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserGroup).IsRequired().HasConversion<string>();
                entity.Property(e => e.PathPattern).IsRequired();
                entity.Property(e => e.Operations).IsRequired().HasConversion<string>();
            });

            // UserPermission (Many-to-Many relationship between User and Permission)
            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId });

                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserPermissions)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Permission)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(e => e.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserProject (Many-to-Many relationship between User and Project)
            modelBuilder.Entity<UserProject>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProjectId });

                entity.HasOne(e => e.User)
                    .WithMany(u => u.AccessibleProjects)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Project)
                    .WithMany(p => p.AuthorizedUsers)
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<UserDepartment>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.DepartmentId });

                entity.HasOne(e => e.User)
                      .WithMany(u => u.UserDepartments)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Department)
                      .WithMany(d => d.UserDepartments)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserProjectGroup>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProjectGroupId });

                entity.HasOne(e => e.User)
                    .WithMany(u => u.AccessibleProjectGroups)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ProjectGroup)
                    .WithMany(d => d.AccessibleUsers)
                    .HasForeignKey(e => e.ProjectGroupId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserRole (Many-to-Many relationship between User and Role)
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // InvitationCode
            modelBuilder.Entity<InvitationCode>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserGroup)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.Code)
                    .IsRequired();

                entity.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.UsedBy)
                    .WithMany()
                    .HasForeignKey(e => e.UsedById)
                    .OnDelete(DeleteBehavior.Restrict); // 当用户被删除时保留邀请码记录

                entity.Property(e => e.ValidDurationHours)
                .HasColumnType("bigint")
                .HasConversion(
                    ts => ts.Ticks,
                    ticks => TimeSpan.FromTicks(ticks));
            });

            modelBuilder.Entity<DrillHole>(entity =>
            {
                entity.ToTable("DrillHoles");
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name)
                        .IsRequired()
                        .HasMaxLength(200);
                entity.Property(d => d.Description).HasMaxLength(1000);
                entity.Property(d => d.StartedTime);
                entity.Property(d => d.EndTime);
                entity.Property(d => d.Status)
                      .HasConversion<string>()
                      .HasMaxLength(50)
                      .IsRequired();
                entity.Property(d => d.DesignedCollarElevation)
                      .HasColumnType("float")
                      .HasComment("设计孔口标高");
                entity.Property(d => d.MeasuredCollarElevation)
                      .HasColumnType("float")
                      .HasComment("实际孔口标高");
                entity.Property(d => d.DesignedCollarX)
                      .HasColumnType("float")
                      .HasComment("设计孔口坐标东");
                entity.Property(d => d.DesignedCollarY)
                      .HasColumnType("float")
                      .HasComment("设计孔口坐标北");
                entity.Property(d => d.MeasuredCollarX)
                      .HasColumnType("float")
                      .HasComment("实际孔口坐标东");
                entity.Property(d => d.MeasuredCollarY)
                      .HasColumnType("float")
                      .HasComment("实际孔口坐标北");
                entity.Property(d => d.CoordinateSystem)
                      .HasConversion<string>()
                      .HasMaxLength(50)
                      .HasComment("坐标系类型");

                // DrillHole - Project: 一对多
                entity.HasOne(d => d.Project)
                        .WithMany(p => p.DrillHoles)
                        .HasForeignKey(d => d.ProjectId)
                        .OnDelete(DeleteBehavior.Cascade);

                // 自引用：重开的父钻孔
                entity.HasOne(d => d.ParentDrillHole)
                        .WithMany()
                        .HasForeignKey(d => d.ParentDrillHoleId)
                        .OnDelete(DeleteBehavior.Restrict);

                // 多对多 Outsources
                entity.HasMany(d => d.Outsources)
                        .WithMany(c => c.DrillHoles)
                        .UsingEntity<Dictionary<string, object>>(
                            "DrillHoleOutsource",
                            j => j.HasOne<Company>()
                                .WithMany()
                                .HasForeignKey("CompanyId")
                                .OnDelete(DeleteBehavior.Cascade),
                            j => j.HasOne<DrillHole>()
                                .WithMany()
                                .HasForeignKey("DrillHoleId")
                                .OnDelete(DeleteBehavior.Cascade),
                            j =>
                            {
                                j.HasKey("DrillHoleId", "CompanyId");
                                j.ToTable("DrillHoleOutsource");
                            });

                // DrillHole - Phases: 一对多
                entity.HasMany(d => d.Phases)
                        .WithOne(p => p.DrillHole)
                        .HasForeignKey(p => p.DrillHoleId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Phase>(entity =>
            {
                entity.ToTable("Phases");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.PhaseType).IsRequired();
                entity.Property(p => p.Status).IsRequired();
                entity.Property(p => p.Order).IsRequired();
                entity.Property(p => p.StartedTime);
                entity.Property(p => p.CompletedTime);

                // Phase - DrillHole FK
                entity.HasOne(p => p.DrillHole)
                    .WithMany(d => d.Phases)
                    .HasForeignKey(p => p.DrillHoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Phase 自引用：SubPhases
                entity.HasOne(p => p.ParentPhase)
                    .WithMany(p => p.SubPhases)
                    .HasForeignKey(p => p.ParentPhaseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CompletionCondition>(entity =>
            {
                entity.HasKey(cc => cc.Id);

                // 一个 CompletionCondition 可包含多个文件资源
                entity.HasMany(cc => cc.RequiredFiles)
                    .WithOne()  // Resource 不需显式导航回 Condition
                    .HasForeignKey("CompletionConditionId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}