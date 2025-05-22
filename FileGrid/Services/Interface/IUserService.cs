using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
    Task<User?> UpdateUserAsync(User user);
    Task<List<User>> GetUsersByCompanyIdAsync(int companyId);
    Task EnsureUserProjectGroupAsync(Guid userId, int projectGroupId);
}
