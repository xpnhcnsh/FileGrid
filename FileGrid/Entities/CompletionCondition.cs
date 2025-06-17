using System;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class CompletionCondition
{
    public Guid Id { get; set; }
    public Guid PhaseId { get; set; }

    public CompletionConditionType ConditionType { get; set; }
    public string? Description { get; set; }
    public bool IsMet { get; set; }

    // Only applies if ConditionType == FileApproval
    public List<ConditionFile>? RequiredFiles { get; set; }
}
