using System;
using System.ComponentModel.DataAnnotations;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class Resource
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string BucketName { get; set; } = null!;
    [Required]
    public string Path { get; set; } = null!;
    [Required]
    public string Version { get; set; } = "0.1"; //默认刚上传的文件版本为0.1
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime UpdatedAt { get; set; }
    [Required]
    public string FileType { get; set; } = null!;
    [Required]
    public FileStatus FileStatus { get; set; } = FileStatus.normal;
    [Required]
    public Guid UploadedBy { get; set; }
    [Required]
    public User Uploader { get; set; } = null!;
    public ICollection<Share> Shares { get; set; } = [];
    public ICollection<ResourcePermission> ResourcePermissions { get; set; } = [];
}
