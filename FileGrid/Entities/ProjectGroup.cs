using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

/// <summary>
/// 项目组Model
/// </summary>
public class ProjectGroup
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Guid> UserIds { get; set; } = [];
    public virtual ICollection<User> Users { get; set; } = [];

}
