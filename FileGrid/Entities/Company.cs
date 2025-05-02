using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class Company
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; } // 例如："CCTEG集团","外协单位A","合作方B"
}
