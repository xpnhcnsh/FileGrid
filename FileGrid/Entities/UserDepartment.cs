using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class UserDepartment
{
    [Required]
    public required Guid UserId { get; set; }
    [Required]
    public required int DepartmentId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
}
