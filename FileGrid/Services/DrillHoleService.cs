using System;
using FileGrid.Entities;
using FileGrid.Services.Interface;

namespace FileGrid.Services;

public class DrillHoleService(FileGridContext context) //: IDrillHoleService
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
}
