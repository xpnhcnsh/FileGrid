using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FileGrid.Utils;
using Microsoft.Identity.Client;

namespace FileGrid.Entities;

public class InvitationCode
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    [ForeignKey(nameof(Creator))]
    public Guid CreatorId { get; set; }
    public virtual User Creator { get; set; } = null!;
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public DateTime ExpiredAt { get; set; }
    [Required]
    public TimeSpan ValidDurationHours { get; set; }  //有效期
    public DateTime? UsedAt { get; set; }
    [Required]
    public bool IsUsed { get; set; } = false;
    [ForeignKey(nameof(UsedBy))]
    public Guid? UsedById { get; set; } //邀请码被某用户在注册时消耗
    [Required]
    public UserGroup UserGroup { get; set; }
    public virtual User? UsedBy { get; set; }
}
