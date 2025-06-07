using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<bool> AddProjectAsync(Project project, IEnumerable<int> outsourceCompanyIds);
}
