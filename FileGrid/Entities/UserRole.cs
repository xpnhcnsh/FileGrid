using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class UserRole
{
    [Key]
    public Guid UserId { get; set; }
    public required User User { get; set; } = null!;
    [Key]
    public required Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
