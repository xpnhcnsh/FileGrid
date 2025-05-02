using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities.Dto;

public class Department
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; } // 例如："项目部","财务部","IT部"
}
