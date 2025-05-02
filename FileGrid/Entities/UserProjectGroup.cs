using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class UserProjectGroup
{
    [Required]
    public required int UserId { get; set; }
    [Required]
    public required int ProjectGroupId { get; set; }
    public virtual User User { get; set; }
    public virtual ProjectGroup Group { get; set; }
}
