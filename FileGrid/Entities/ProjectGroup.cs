using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

/// <summary>
/// 项目组
/// </summary>
public class ProjectGroup
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<UserProjectGroup> UserProjectGroups { get; set; } = [];
}
