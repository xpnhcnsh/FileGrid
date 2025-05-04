using System;

namespace FileGrid.Entities;

public class ShareResource
{
    public int ShareId { get; set; }
    public int ResourceId { get; set; }
    public virtual Share Share { get; set; } = null!;
    public virtual Resource Resource { get; set; } = null!;
}
