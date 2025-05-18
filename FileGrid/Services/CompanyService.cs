using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Services;

public class CompanyService(FileGridContext context) : ICompanyService
{
    private readonly FileGridContext _context = context;
    public async Task<Company?> AddCompanyAsync(Company company)
    {
        var entry = await _context.Companies.AddAsync(company);
        int affected = await _context.SaveChangesAsync();
        return affected > 0 ? entry.Entity : null;
    }

    public Task<Department?> AddDepartmentAsync(Department dep)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCompanyByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies.Include(x => x.Departments)
        .OrderBy(x => x.Name).ToListAsync();
    }

    public Task<List<Department>> GetAllDepartmentsAsync()
    {
        return Task.FromResult(new List<Department>());
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Companies.AnyAsync(x => x.Name == name);
    }
}
