using HFT_26271_S10.ProjectManager.Models.Classes;
using HFT_26271_S10.ProjectManager.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.BusinessLogic.Interfaces
{
    public interface IProjectTaskService
    {
        List<ProjectTask> GetAllTasks();
        ProjectTask? GetTaskById(int id);
        bool CreateTask(ProjectTask task);
        bool UpdateTask(ProjectTask task);
        bool DeleteTask(int id);
        List<ProjectTask> GetHighOrCriticalPriorityTasks();
        List<string> GetProjectTaskNames();
        bool HasOverdueTasks();
        int GetCompletedTaskCount();
        List<ProjectTask> GetTasksByProjectId(int projectId);
        ProjectTask? GetNextTask();
        List<ProjectTaskCountDto> GetTaskCountByProject();
        List<TaskStateCountDto> GetTaskCountByState();
        List<PriorityStatisticsDto> GetPriorityStatistics();
        List<ProjectStatisticsDto> GetProjectStatistics();
    }
}
