using System;
using FileGrid.Utils;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class Phase
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid? ParentPhaseId { get; set; }

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public TasknPhaseStatus Status { get; set; }
    public PhaseType PhaseType { get; set; }
    public int OrderIndex { get; set; }
    public DateTime? CompletedTime { get; set; }
    public DateTime? StartedTime { get; set; }

    public List<Phase> SubPhases { get; set; } = [];
    public List<PhaseParameter> Parameters { get; set; } = [];
    public List<CompletionCondition> CompletionConditions { get; set; } = new();
}
