using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class Permission
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string PermissionName { get; set; } = null!;

    public ICollection<User> Users { get; set; } = [];
}
