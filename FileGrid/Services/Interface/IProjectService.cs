using System;
using FileGrid.Entities;

namespace FileGrid.Services.Interface;

public interface IProjectService
{
    Task<List<Project>> GetAllProjectsAsync();
}
