using System;
using System.ComponentModel.DataAnnotations;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class CompletionCondition
{
    [Key]
    public Guid Id { get; set; }
    public Guid PhaseId { get; set; }

    public CompletionConditionType ConditionType { get; set; }
    public string? Description { get; set; }
    public bool IsMet { get; set; }

    // Only applies if ConditionType == FileApproval
    public virtual ICollection<Resource> RequiredFiles { get; set; } = [];
}
