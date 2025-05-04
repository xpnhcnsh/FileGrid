using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class Permission
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string PermissionName { get; set; } = null!;

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = [];
}
