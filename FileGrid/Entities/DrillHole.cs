using System.ComponentModel.DataAnnotations;
using FileGrid.Utils.Enum;

namespace FileGrid.Entities;

public class DrillHole
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    public Guid? ParentDrillHoleId { get; set; } //指向上一个被废弃的钻孔
    public DrillHole? ParentDrillHole { get; set; } //指向上一个被废弃的钻孔
    public string? Description { get; set; }
    public DateTime? StartedTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DrillHoleStatus Status { get; set; }

    public virtual ICollection<Phase> Phases { get; set; } = [];
    public virtual ICollection<Company> Outsources { get; set; } = [];
}
