using System;
using System.ComponentModel.DataAnnotations;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class Resource
{
    [Key]
    public int Id { get; set; }
    [Required]
    public required string BucketName { get; set; } = null!;
    [Required]
    public required string Path { get; set; } = null!;
    public required string Version { get; set; } = "0.1"; //默认刚上传的文件版本为0.1
    [Required]
    public required DateTime CreatedAt { get; set; }
    [Required]
    public required DateTime UpdatedAt { get; set; }
    [Required]
    public string FileType { get; set; } = null!;
    [Required]
    public required FileStatus FileStatus { get; set; } = FileStatus.normal;
    [Required]
    public required Guid CreatedById { get; set; }
    public required User Creator { get; set; } = null!;
    public ICollection<Share> Shares { get; set; } = [];
}
