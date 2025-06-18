using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface IDrillHoleService
{
    Task<DrillHole> AbandonAndRestart(Guid drillHoleId);
}
