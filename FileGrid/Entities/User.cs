using System.ComponentModel.DataAnnotations;
using FileGrid.Entities.Dto;
using FileGrid.Utils;

namespace FileGrid.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required AccountType AccountType { get; set; }
    [Required]
    public required DateTime CreatedAt { get; set; }
    [Required]
    public required DateTime UpdatedAt { get; set; }
    public string PasswordHash { get; set; } = null!;
    // 组织信息（所有用户）
    public int CompanyId { get; set; }       // 所属公司
    public virtual Company? Company { get; set; }
    public int DepartmentId { get; set; }    // 所属部门
    public virtual Department? Department { get; set; }
    public string? JobTitle { get; set; }     // 职务名称

    // 角色类型（仅CCTEG用户需要）
    public int? UserGroupId { get; set; }
    public virtual UserGroup? UserGroup { get; set; }
    public ICollection<Role> Roles { get; set; } = [];
    public ICollection<Share> Shares { get; set; } = [];
    public ICollection<Share> SharedWithMe { get; set; } = [];
    public ICollection<Resource> UploadedResources { get; set; } = [];
    public ICollection<Permission> Permissions { get; set; } = [];
    // 关联表（权限控制）
    public virtual ICollection<UserProjectGroup> AccessibleProjectGroup { get; set; }  // L2可访问项目组
    public virtual ICollection<UserProject> AccessibleProjects { get; set; }     // L3可访问项目
}


