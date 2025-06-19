using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FileGrid.Services;

public class DrillHoleService(FileGridContext context) : IDrillHoleService
{
    private readonly FileGridContext _context = context;

    // public async Task<DrillHole> AbandonAndRestart(Guid drillHoleId)
    // {
    //     // 1. 获取旧 DrillHole 实例（包括关联结构）
    //     var old = await _context.DrillHoles
    //         .Include(d => d.Phases).ThenInclude(p => p.SubPhases)
    //         .Include(d => d.Outsources)
    //         .Include(d => d.Project)
    //         .FirstOrDefaultAsync(d => d.Id == drillHoleId);

    //     if (old == null)
    //         throw new ArgumentException("未找到指定钻孔");

    //     // 2. 创建旧实例的深度克隆
    //     var clone = old.DeepClone();

    //     // 3. 配置新实例属性
    //     old.Status = DrillHoleStatus.Abandoned;
    //     clone.Id = Guid.NewGuid();
    //     clone.Status = DrillHoleStatus.NotStarted;
    //     clone.StartedTime = null;
    //     clone.EndTime = null;
    //     clone.ParentDrillHoleId = old.Id;

    //     // 确保关联到同项目
    //     clone.ProjectId = old.ProjectId;
    //     clone.Project = old.Project;

    //     // 4. 重置所有阶段状态
    //     foreach (var p in clone.Phases)
    //         ResetPhaseRecursive(p);

    //     // 5. 保存更改
    //     _context.DrillHoles.Update(old);
    //     _context.DrillHoles.Add(clone);
    //     old.Project.DrillHoles.Add(clone);  // 将新 DrillHole 加进导航集合

    //     await _context.SaveChangesAsync();
    //     return clone;
    // }

    // private void ResetPhaseRecursive(Phase phase)
    // {
    //     phase.Id = Guid.NewGuid();  // 新标识
    //     phase.Status = PhaseStatus.NotStarted;
    //     phase.StartedTime = null;
    //     phase.CompletedTime = null;

    //     foreach (var sub in phase.SubPhases)
    //         ResetPhaseRecursive(sub);
    // }

    public async Task<bool> AddDrillHole(DrillHole hole)
    {
        if (hole == null || string.IsNullOrWhiteSpace(hole.Name))
            return false;
        try
        {
            await _context.DrillHoles.AddAsync(hole);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> AddOrUpdateAsync(DrillHole hole)
    {
        if (hole == null || string.IsNullOrWhiteSpace(hole.Name))
            return false;

        try
        {
            if (hole.Id == Guid.Empty)
            {
                // 新增
                _context.DrillHoles.Add(hole);
            }
            else
            {
                var existing = await _context.DrillHoles.FirstOrDefaultAsync(x => x.Id == hole.Id);
                if (existing == null)
                    return false;
                _context.DrillHoles.Entry(existing).CurrentValues.SetValues(hole);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteDrillHole(Guid id)
    {
        if (id == Guid.Empty)
            return false;

        try
        {
            var existing = await _context.DrillHoles.FirstOrDefaultAsync(h => h.Id == id);
            if (existing == null)
                return false;

            _context.DrillHoles.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
