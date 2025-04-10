using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class Role
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public required string RoleName { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<User> Users { get; set; } = [];
}
