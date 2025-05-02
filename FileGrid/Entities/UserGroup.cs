using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class UserGroup
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(20)]
    public string Code { get; set; } // CCTEGL1~CCTEGL5

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;
}
