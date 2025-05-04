using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class ProjectPartA
{
    [Required]
    public int ProjectId { get; set; }  // 项目ID
    [Required]
    public int PartAId { get; set; }  // Part A ID
    public virtual Project Project { get; set; } = null!;  // 导航属性，关联到Project实体
    public virtual Company PartA { get; set; } = null!;  // 导航属性，关联到PartA实体
}
