using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;

namespace FileGrid.Services;

public class CompanyService : ICompanyService
{
    public Task AddAsync(Company company)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Company>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
