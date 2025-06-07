using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Services;

public class ProjectService(FileGridContext context) : IProjectService
{
    private readonly FileGridContext _context = context;

    /// <summary>
    /// 获取所有项目
    /// </summary>
    /// <returns>返回所有项目列表</returns>
    public async Task<List<Project>> GetAllProjectsAsync()
    {
        return await _context.Projects
            .Include(x => x.PartA)
            .Include(x => x.Outsources)
            .Include(x => x.Manager)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> AddProjectAsync(Project project, IEnumerable<int> outsourceCompanyIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            if (outsourceCompanyIds.Any())
            {
                foreach (var companyId in outsourceCompanyIds)
                {
                    _context.ProjectOutsources.Add(new ProjectOutsource
                    {
                        ProjectId = project.Id,
                        OutsourceId = companyId
                    });
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            return false;
            throw;
        }
    }
}
