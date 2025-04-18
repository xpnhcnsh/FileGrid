using System;

namespace FileGrid.Services.Interface;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string storedHash);
}
