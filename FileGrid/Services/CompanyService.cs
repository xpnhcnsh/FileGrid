using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;

namespace FileGrid.Services;

public class CompanyService : ICompanyService
{
    public Task<Company?> AddCompanyAsync(Company company)
    {
        throw new NotImplementedException();
    }

    public Task<Department?> AddDepartmentAsync(Department dep)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCompanyByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Company>> GetAllCompaniesAsync()
    {
        return Task.FromResult(new List<Company>());
    }

    public Task<List<Department>> GetAllDepartmentsAsync()
    {
        return Task.FromResult(new List<Department>());
    }
}
