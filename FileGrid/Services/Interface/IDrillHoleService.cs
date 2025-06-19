using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface IDrillHoleService
{
    //Task<DrillHole> AbandonAndRestart(Guid drillHoleId);

    Task<bool> AddDrillHole(DrillHole hole);

    Task<bool> AddOrUpdateAsync(DrillHole hole);

    Task<bool> DeleteDrillHole(Guid id);


}
