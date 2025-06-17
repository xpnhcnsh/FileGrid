using System;

namespace FileGrid.Utils;

public class PhaseParameter
{
    public Guid Id { get; set; }
    public Guid PhaseId { get; set; }
    public string Name { get; set; } = default!;
    public string? Value { get; set; }

}
