using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid ProjectId { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public User CreatedBy { get; set; } = null!;
    public TasknPhaseStatus Status { get; set; }

    public List<Phase> Phases { get; set; } = [];
}
