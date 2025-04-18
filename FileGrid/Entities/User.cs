using System.ComponentModel.DataAnnotations;
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
    public ICollection<Role> Roles { get; set; } = [];
    public ICollection<Share> Shares { get; set; } = [];
    public ICollection<Share> SharedWithMe { get; set; } = [];
    public ICollection<Resource> UploadedResources { get; set; } = [];
    public ICollection<Permission> Permissions { get; set; } = [];
}


