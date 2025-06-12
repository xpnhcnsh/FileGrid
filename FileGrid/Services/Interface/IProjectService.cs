using System;
using FileGrid.Entities;
using FileGrid.Utils.Enum;

namespace FileGrid.Services.Interface;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<bool> AddProjectAsync(Project project);
    Task<bool> UpdateProjectAsync(Project project);
    Task<Project?> GetProjectByIdAsync(int projectId);
    ErrorCode ValidateAProject(Project project);
}
