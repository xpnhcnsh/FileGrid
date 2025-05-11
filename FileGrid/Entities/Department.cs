using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

/// <summary>
/// 部门表
/// </summary>
public class Department
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty; // 例如："项目部","财务部","IT部"
    public string? Description { get; set; } // 部门描述
    public int CompanyId { get; set; } // 所属公司ID
    public virtual Company Company { get; set; } = null!; // 所属公司
}
