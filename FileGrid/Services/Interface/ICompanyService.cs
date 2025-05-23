using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface ICompanyService
{
    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company?> AddCompanyAsync(Company company);
    Task<bool> DeleteCompanyByIdAsync(int id);
    Task<List<Department>> GetAllDepartmentsAsync();
    Task<Department?> AddDepartmentAsync(Department dep);
    Task<bool> ExistsByNameAsync(string name);

    Task<List<Department>> GetDepartmentsByCompanyIdAsync(int companyId);
    Task<Department?> UpdateDepartmentAsync(Department dep);
    Task<User?> AddEmployeeAsync(User user);
    Task<User?> UpdateEmployeeAsync(User user);
    Task<bool> DeleteEmployeeByIdAsync(Guid userId);
    Task<bool> DeleteDepartmentByIdAsync(int depId);
}
