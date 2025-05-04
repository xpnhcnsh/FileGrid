using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class UserProjectGroup
{
    [Required]
    public required Guid UserId { get; set; }
    [Required]
    public required int ProjectGroupId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual ProjectGroup Group { get; set; } = null!;
}
