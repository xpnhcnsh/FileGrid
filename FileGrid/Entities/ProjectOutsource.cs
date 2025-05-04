using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class ProjectOutsource
{
    [Required]
    public int ProjectId { get; set; }  // 项目ID
    [Required]
    public int OutsourceId { get; set; }  // 外包公司ID
    public virtual Project Project { get; set; } = null!;  // 导航属性，关联到Project实体
    public virtual Company Outsource { get; set; } = null!;  // 导航属性，关联到Outsource实体
}
