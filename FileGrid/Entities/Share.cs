using System.ComponentModel.DataAnnotations;
using FileGrid.Utils;

namespace FileGrid.Entities;


public class Share
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required Guid OwnerId { get; set; }   // 分享者（例如用户A）的标识
    public virtual User Owner { get; set; } = null!;
    [Required]
    public required ShareType ShareType { get; set; }
    public string? ShareLink { get; set; }// 如果是匿名分享，生成唯一的分享链接标识
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }  // 可选的过期时间
                                              // 目前规定分享仅允许下载，即不允许修改、删除等操作
    public virtual ICollection<ShareResource> ShareResources { get; set; } = [];
    public virtual ICollection<ShareUser> ShareUsers { get; set; } = [];
}
