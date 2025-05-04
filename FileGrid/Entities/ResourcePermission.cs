using System;
using System.ComponentModel.DataAnnotations;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

// 核心权限表
public class ResourcePermission
{
    public int Id { get; set; }

    // 权限主体配置
    public PrincipalType PrincipalType { get; set; }
    public string PrincipalTag { get; set; } = null!; // 格式："User:1001" 或 "Group:CCTEGL3"

    // 资源定位
    public ResourceType ResourceType { get; set; }
    [Required]
    public string ResourcePath { get; set; } = null!; // 统一格式：bucket/path/to/folder/ 或 bucket/path/to/file.txt

    // 权限配置
    public ResourceOperation Operations { get; set; }
    public bool ApplyToSubItems { get; set; }  // 是否应用到子资源（继承）
    public DateTime? ExpireTime { get; set; }

    // 审计字段
    public Guid? GrantedById { get; set; }  // 授权人ID
    public User? GrantedBy { get; set; }     // 授权人
    public DateTime GrantTime { get; set; } = DateTime.UtcNow;
}
