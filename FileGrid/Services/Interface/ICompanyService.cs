using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface ICompanyService
{
    Task<List<Company>> GetAllAsync();
    Task AddAsync(Company company);
    Task<bool> DeleteByIdAsync(int id);
}
