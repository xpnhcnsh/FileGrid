using System;
using System.ComponentModel.DataAnnotations;
using FileGrid.Utils;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class Phase
{
    [Key]
    public Guid Id { get; set; }
    public Guid DrillHoleId { get; set; }
    public DrillHole DrillHole { get; set; } = default!;
    public Guid? ParentPhaseId { get; set; }
    public Phase? ParentPhase { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public PhaseStatus Status { get; set; }
    public PhaseType PhaseType { get; set; }
    public int Order { get; set; }
    public DateTime? CompletedTime { get; set; }
    public DateTime? StartedTime { get; set; }

    public List<Phase> SubPhases { get; set; } = [];
    public List<PhaseParameter> Parameters { get; set; } = [];
    public List<CompletionCondition> CompletionConditions { get; set; } = [];
}
