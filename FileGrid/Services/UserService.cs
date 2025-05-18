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
        var res = await _context.Users.AsNoTracking()
            .Include(u => u.AccessibleProjectGroups)
            .Include(u => u.Company)
            .Include(u => u.Department)
            .Include(u => u.AccessibleProjectGroups)
            .Include(u => u.AccessibleProjects)
            .FirstOrDefaultAsync(u => u.Id == userId);
        return res ?? null;
    }

    public async Task<List<User>> GetUsersByCompanyIdAsync(int companyId)
    {
        return await _context.Users.AsNoTracking()
            .Include(u => u.AccessibleProjectGroups)
            .Include(u => u.Company)
            .Include(u => u.Department)
            .Include(u => u.AccessibleProjectGroups)
            .Include(u => u.AccessibleProjects)
            .Where(u => u.CompanyId == companyId).ToListAsync();
    }

    public Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        var toUpdateCount = await _context.Users.Where(x => x.Id == user.Id)
        .ExecuteUpdateAsync(set => set
        .SetProperty(u => u.Name, _ => user.Name)
        .SetProperty(u => u.Email, _ => user.Email)
        .SetProperty(u => u.PhoneNumber, _ => user.PhoneNumber)
        .SetProperty(u => u.CompanyId, _ => user.CompanyId)
        .SetProperty(u => u.DepartmentId, _ => user.DepartmentId)
        .SetProperty(u => u.JobTitle, _ => user.JobTitle)
        );
        return toUpdateCount > 0 ? user : null;
    }

}
