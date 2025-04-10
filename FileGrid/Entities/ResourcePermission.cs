using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class ResourcePermission
{
    // 对某个资源，该条记录表示一个用户或角色具有什么权限
    [Key]
    public int Id { get; set; }
    [Required]
    public int ResourceId { get; set; }
    public required Resource Resource { get; set; }

    // 如果 RoleId 和 UserId 都有值，则以用户为准；也可以选择一种设计：二选一（例如角色权限与用户权限分两张表）
    public int? RoleId { get; set; }    // 可为空，表示不适用角色权限
    public Role? Role { get; set; }

    public string? UserId { get; set; }  // 可为空，表示不适用于单一用户的权限
    public User? User { get; set; }
    [Required]
    public int PermissionId { get; set; }
    public required Permission Permission { get; set; }
}
