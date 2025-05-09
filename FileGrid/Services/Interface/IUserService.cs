using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
}
