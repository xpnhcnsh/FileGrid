using System;
using System.ComponentModel.DataAnnotations;

namespace FileGrid.Entities;

public class ProjectResource
{
    [Key]
    public int ProjectId { get; set; }  // 项目ID
    [Required]
    public string MinioBucket { get; set; } = null!;  // Minio存储桶
    [Required]
    public string BasePath { get; set; } = string.Empty;  // 基础路径
    public virtual Project Project { get; set; } = null!;  // 导航属性，关联到Project实体
    public ICollection<ProjectFile> Files { get; set; } = [];  // 关联的项目文件集合
}
