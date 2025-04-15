using System.ComponentModel.DataAnnotations;
using FileGrid.Utils;

namespace FileGrid.Entities;


public class Share
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required Guid OwnerUserId { get; set; }   // 分享者（例如用户A）的标识
    public required User OwnerUser { get; set; }
    [Required]
    public required ShareType ShareType { get; set; }
    // 如果是匿名分享，生成唯一的分享链接标识（例如 GUID）
    public string? ShareLink { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }  // 可选的过期时间
                                              // 目前规定分享仅允许下载，即不允许修改、删除等操作
    public ICollection<Resource> Resources { get; set; } = [];
    public ICollection<User> TargetUsers { get; set; } = [];
    public ICollection<Guid> TargetUserIds { get; set; } = [];
    [Required]
    public ICollection<int> ResourceIds { get; set; } = [];
}
