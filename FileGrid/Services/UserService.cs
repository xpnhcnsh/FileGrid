using System;
using System.Reflection.Metadata.Ecma335;
using FileGrid.Entities;
using FileGrid.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Services;

public class UserService(FileGridContext context) : IUserService
{
    private readonly FileGridContext _context = context;

    public Task<User?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        var res = await _context.Users
            .Include(u => u.AccessibleProjectGroups)
            .Include(u => u.Company)
            .Include(u => u.Department)
            .Include(u => u.AccessibleProjectGroups)
            .Include(u => u.AccessibleProjects)
            .FirstOrDefaultAsync(u => u.Id == userId);
        res ??= null;
        return res;
    }

    public Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}
