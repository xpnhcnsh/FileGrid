using System;

namespace FileGrid.Entities;

public class ProjectFile
{
    public int Id { get; set; }  // 文件ID
    public string RelativePath { get; set; } = string.Empty;  // MinIO中的相对路径
    public string FileName { get; set; } = string.Empty;  // 文件名
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // 创建时间
    public long FileSize { get; set; }  // 文件大小（字节）
    public int ProjectId { get; set; }  // 关联的项目ID
    public int ProjectResourceId { get; set; }  // 关联的项目资源ID
    public virtual ProjectResource ProjectResource { get; set; } = null!;  // 导航属性，关联到ProjectResource实体
}
