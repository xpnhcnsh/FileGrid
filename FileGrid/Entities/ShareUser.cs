using System;

namespace FileGrid.Entities;

public class ShareUser
{
    public int ShareId { get; set; }
    public Guid UserId { get; set; }
    public virtual Share Share { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
