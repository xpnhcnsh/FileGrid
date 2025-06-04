using FileGrid.Entities;
using FileGrid.Services.Interface;
using FileGrid.Utils.Enum;
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

    public async Task EnsureUserProjectGroupAsync(Guid userId, int projectGroupId)
    {
        var exists = await _context.UserProjectGroups
            .AnyAsync(upg => upg.UserId == userId && upg.ProjectGroupId == projectGroupId);

        if (!exists)
        {
            _context.UserProjectGroups.Add(new UserProjectGroup
            {
                UserId = userId,
                ProjectGroupId = projectGroupId
            });
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<User>> GetCCTEGEmployeesAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.Company)
            .Where(u => u.Company != null && u.Company.Type == CompanyType.CCTEG)
            .ToListAsync();
    }

    public async Task<List<User>> GetCCTEGEmployeeByNameAsync(string name)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.Company)
            .Where(u => u.Company != null && u.Company.Type == CompanyType.CCTEG && u.Name == name)
            .ToListAsync();
    }
}
