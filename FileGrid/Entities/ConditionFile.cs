using System;

namespace FileGrid.Entities;

public class ConditionFile
{
    public Guid Id { get; set; }
    public Guid CompletionConditionId { get; set; }

    public string FileName { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public string FileType { get; set; } = default!;
    public bool IsApproved { get; set; }
    public DateTime? ApprovedTime { get; set; }
    public DateTime UploadedTime { get; set; }
    public User UploadedUser { get; set; } = null!;
}
