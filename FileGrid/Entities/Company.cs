using System.ComponentModel.DataAnnotations;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class Company
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public CompanyType Type { get; set; } // 公司类型
    public string? Address { get; set; } = string.Empty; // 公司地址
    public string? Description { get; set; } = string.Empty;
    public User? ContactUser { get; set; }
    public Guid? ContactUserId { get; set; }
    public string? TaxCode { get; set; } = string.Empty; // 税号
    public virtual ICollection<User> Users { get; set; } = [];
    public virtual ICollection<ProjectOutsource> ProjectsOutSources { get; set; } = []; //作为外协单位参与的项目
    public virtual ICollection<ProjectPartA> ProjectPartAs { get; set; } = []; //作为甲方参与的项目
    public virtual ICollection<Department> Departments { get; set; } = []; // 公司下的部门
    public virtual ICollection<DrillHole> DrillHoles { get; set; } = []; //作为外协单位管理的井场
}
