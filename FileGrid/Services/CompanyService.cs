using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;
using FileGrid.Utils.Enum;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Services;

public class CompanyService(FileGridContext context) : ICompanyService
{
    private readonly FileGridContext _context = context;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    public async Task<Company?> AddCompanyAsync(Company company)
    {
        var entry = await _context.Companies.AddAsync(company);
        int affected = await _context.SaveChangesAsync();
        return affected > 0 ? entry.Entity : null;
    }

    public async Task<bool> DepartmentExistsByNameAsync(string name)
    {
        return await _context.Departments.AnyAsync(x => x.Name == name);
    }

    public async Task<Department?> AddDepartmentAsync(Department dep)
    {
        if (await DepartmentExistsByNameAsync(dep.Name))
            return null;
        await _context.Departments.AddAsync(dep);
        int affected = await _context.SaveChangesAsync();
        return affected > 0 ? dep : null;
    }

    public async Task<List<Department>> GetAllProjectGroupsAsync()
    {
        return await _context.Departments.AsNoTracking()
            .Where(x => x.IsProjectGroup == true)
            .OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<bool> DeleteCompanyByIdAsync(int id)
    {
        var affectedRows = await _context.Companies.Where(x => x.Id == id).ExecuteDeleteAsync();
        return affectedRows > 0;
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies.AsNoTracking().Include(x => x.Departments)
        .OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<List<Company>> GetPartAsByNameAsync(string name)
    {
        await _semaphore.WaitAsync();
        try
        {
            return await _context.Companies.AsNoTracking()
                                 .Where(x => x.Type == CompanyType.PartA && EF.Functions.Like(x.Name, $"%{name}%"))
                                 .OrderBy(x => x.Name).ToListAsync();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<List<Company>> GetOutSourcesByNameAsync(string name)
    {
        await _semaphore.WaitAsync();
        try
        {
            return await _context.Companies.AsNoTracking()
        .Where(x => x.Type == CompanyType.Outsource && EF.Functions.Like(x.Name, $"%{name}%"))
        .OrderBy(x => x.Name).ToListAsync();
        }
        finally
        {
            _semaphore.Release();
        }
    }


    public async Task<List<Company>> GetAllPartACompaniesAsync()
    {
        return await _context.Companies.AsNoTracking().Where(x => x.Type == CompanyType.PartA)
        .OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<List<Company>> GetAllOutSourceCompaniesAsync()
    {
        return await _context.Companies.AsNoTracking().Where(x => x.Type == CompanyType.Outsource)
        .OrderBy(x => x.Name).ToListAsync();
    }


    public async Task<List<Company>> GetAllCCTEGCompaniesAsync()
    {
        return await _context.Companies.AsNoTracking().Where(x => x.Type == CompanyType.CCTEG)
        .OrderBy(x => x.Name).ToListAsync();
    }


    public async Task<List<Department>> GetAllDepartmentsAsync()
    {
        return await _context.Departments.AsNoTracking().ToListAsync();
    }

    public async Task<List<Department>> GetDepartmentsByCompanyIdAsync(int companyId)
    {
        return await _context.Departments
        .AsNoTracking()
        .Where(x => x.CompanyId == companyId)
        .ToListAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Companies.AsNoTracking().AnyAsync(x => x.Name == name);
    }

    public async Task<Department?> UpdateDepartmentAsync(Department dep)
    {
        var affected = await _context.Departments
        .Where(x => x.Id == dep.Id)
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(d => d.Name, dep.Name)
            .SetProperty(d => d.Description, dep.Description)
        );
        return affected > 0 ? dep : null;
    }

    public Task<User?> AddEmployeeAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UpdateEmployeeAsync(User user)
    {
        var affected = await _context.Users
        .Where(x => x.Id == user.Id)
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(u => u.Name, user.Name)
            .SetProperty(u => u.Email, user.Email)
            .SetProperty(u => u.JobTitle, user.JobTitle)
        );
        return affected > 0 ? user : null;
    }

    public Task<bool> DeleteEmployeeByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteDepartmentByIdAsync(int depId)
    {
        var affectedRows = await _context.Departments.Where(x => x.Id == depId).ExecuteDeleteAsync();
        return affectedRows > 0;
    }
}
