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
    public UserGroup UserGroup { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
    [Required]
    public string PasswordHash { get; set; } = null!;

    // 组织信息（所有用户）
    public int? CompanyId { get; set; }       // 所属公司
    public virtual Company? Company { get; set; }
    public int? DepartmentId { get; set; }    // 所属部门
    public virtual Department? Department { get; set; }
    public string? JobTitle { get; set; }     // 职务名称
    public string? PhoneNumber { get; set; }  // 手机号码
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<UserPermission> UserPermissions { get; set; } = [];
    public ICollection<Share> Shares { get; set; } = [];
    public ICollection<ShareUser> SharedWithMe { get; set; } = [];
    public ICollection<Resource> UploadedResources { get; set; } = [];
    public virtual ICollection<UserDepartment> UserDepartments { get; set; } = [];
    public virtual ICollection<UserProjectGroup> AccessibleProjectGroups { get; set; } = [];
    public virtual ICollection<UserProject> AccessibleProjects { get; set; } = [];     // L3可访问项目
}


