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
    public bool IsProjectGroup { get; set; } = false; // 是否是项目组
    public virtual Company Company { get; set; } = null!; // 所属公司
    public virtual ICollection<Project> Projects { get; set; } = []; // 若是项目组，则拥有的项目
    public virtual ICollection<UserDepartment> UserDepartments { get; set; } = [];
    // 用户可访问项目组关系（当IsProjectGroup=true时）
    public virtual ICollection<UserProjectGroup> AccessibleUsers { get; set; } = [];

}
