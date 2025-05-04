using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class UserProject
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public int ProjectId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;

}
