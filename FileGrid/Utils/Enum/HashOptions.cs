using System;

namespace FileGrid.Utils.Enum;

public class HashOptions
{
    public int Iterations { get; set; } = 210000;
    public int SaltSize { get; set; } = 32;  // 256-bit
    public int HashSize { get; set; } = 64;  // 512-bit
}
