using System;
using System.Security.Cryptography;
using System.Text;
using FileGrid.Services.Interface;
using FileGrid.Utils.Enum;
using Microsoft.Extensions.Options;

namespace FileGrid.Services;

public class AuthService : IAuthService
{
    private readonly HashOptions HashOptions;

    public AuthService(IOptions<HashOptions> Hashoptions)
    {
        HashOptions = Hashoptions.Value;
    }

    public string HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] salt = new byte[HashOptions.SaltSize];
        rng.GetBytes(salt);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            HashOptions.Iterations,
            HashAlgorithmName.SHA512,
            HashOptions.HashSize
        );

        return $"{HashOptions.Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        string[] parts = storedHash.Split(':', 3);
        if (parts.Length != 3) throw new FormatException("Invalid hash format");

        int iterations = int.Parse(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] originalHash = Convert.FromBase64String(parts[2]);

        byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            originalHash.Length
        );

        return CryptographicOperations.FixedTimeEquals(originalHash, computedHash);
    }
}
