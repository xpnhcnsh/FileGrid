using FileGrid.Entities;
using FileGrid.Services.Interface;
using FileGrid.Utils.Enum;
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
                .ThenInclude(po => po.Outsource)
            .Include(x => x.Manager)
            .Include(x => x.DeputyManager)
            .Include(x => x.SafetyLeader)
            .Include(x => x.ProductionLeader)
            .Include(x => x.TechnicalLeader)
            .Include(x => x.ProjectGroup)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> AddProjectAsync(Project project)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return false;
            throw;
        }
    }

    public async Task<bool> UpdateProjectAsync(Project project)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // 更新主表字段
            _context.Projects.Update(project);

            // 删除旧的外协关系
            var existingOutsources = _context.ProjectOutsources
                .Where(po => po.ProjectId == project.Id);
            _context.ProjectOutsources.RemoveRange(existingOutsources);

            // 添加新的外协关系
            if (project.Outsources != null && project.Outsources.Any())
            {
                await _context.ProjectOutsources.AddRangeAsync(project.Outsources);
            }

            // 提交更改
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // 可选：记录日志 ex.Message 或 ex.ToString()
            return false;
        }
    }

    public async Task<Project?> GetProjectByIdAsync(int projectId)
    {
        return await _context.Projects
            .Include(x => x.PartA)
            .Include(x => x.Outsources)
                .ThenInclude(po => po.Outsource)
            .Include(x => x.Manager)
            .Include(x => x.DeputyManager)
            .Include(x => x.SafetyLeader)
            .Include(x => x.ProductionLeader)
            .Include(x => x.TechnicalLeader)
            .Include(x => x.ProjectGroup)
            .SingleOrDefaultAsync(x => x.Id == projectId);
    }

    public ErrorCode ValidateAProject(Project project)
    {
        // 检查各负责人是否为空
        if (project.ManagerId == null)
        {
            return ErrorCode.EmptyManager;
        }

        if (project.SafetyLeaderId == null)
        {
            return ErrorCode.EmptySafetyDirector;
        }

        if (project.ProductionLeaderId == null)
        {
            return ErrorCode.EmptyProductionDirector;
        }

        if (project.TechnicalLeaderId == null)
        {
            return ErrorCode.EmptyTechnicalDirector;
        }

        // 检查负责人是否有重复
        var ids = new List<Guid?>
        {
            project.ManagerId,
            project.DeputyManagerId,
            project.SafetyLeaderId,
            project.ProductionLeaderId,
            project.TechnicalLeaderId
        };
        if (ids.GroupBy(x => x).Any(g => g.Count() > 1))
        {
            return ErrorCode.DuplicateDirectors;
        }

        // 检查甲方是否为空
        var partAId = project.PartAId;
        if (partAId == null)
        {
            return ErrorCode.EmptyPartA;
        }

        // 检查外协施工方是否为空
        if (!project.Outsources.Any())
        {
            return ErrorCode.EmptyOutsources;
        }

        // 无错误
        return ErrorCode.None;
    }
}
