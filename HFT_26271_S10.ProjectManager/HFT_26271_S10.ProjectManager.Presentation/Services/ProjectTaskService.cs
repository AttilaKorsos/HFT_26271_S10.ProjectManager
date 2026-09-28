using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HFT_26271_S10.ProjectManager.Models.Classes;
using HFT_26271_S10.ProjectManager.Presentation.DataProviders;
using HFT_26271_S10.ProjectManager.Presentation.DTOs;
using HFT_26271_S10.ProjectManager.Models.Enums;
using HFT_26271_S10.ProjectManager.Presentation.Extensions;

namespace HFT_26271_S10.ProjectManager.Presentation.Services
{
    public class ProjectTaskService
    {
        private readonly ProjectTaskJsonDataProvider dataProvider;
        public ProjectTaskService(ProjectTaskJsonDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public List<ProjectTask> GetAllTasks()
        {
            return dataProvider.GetAll();
        }

        public ProjectTask? GetTaskById(int id)
        {
            return dataProvider.GetById(id);
        }

        public bool CreateTask(ProjectTask task)
        {
            if (string.IsNullOrWhiteSpace(task.Name)) return false;

            dataProvider.Add(task);
            return true;
        }

        public bool UpdateTask(ProjectTask task)
        {
            if (string.IsNullOrWhiteSpace(task.Name)) return false;

            return dataProvider.Update(task);
        }

        public bool DeleteTask(int id)
        {
            return dataProvider.Delete(id);
        }

        //3.4. LINQ
        public List<ProjectTask> GetHighOrCriticalPriorityTasks()
        {
            return dataProvider.GetAll()
                .Where(task => task.Priority == TaskPriority.High || task.Priority == TaskPriority.Critical)
                .ToList();
        }

        public List<ProjectTask> GetInProgressTasksOrderedByName()
        {
            return dataProvider.GetAll()
                .Where(task => task.State == TaskState.InProgress)
                .OrderBy(task => task.Name)
                .ToList();
        }

        public List<string> GetProjectTaskNames()
        {
            return dataProvider.GetAll()
                .Select(task => task.Name)
                .ToList();
        }

        public bool HasOverdueTasks()
        {
            return dataProvider.GetAll()
                .Any(task => task.State != TaskState.Completed &&
                    task.DueDate.HasValue &&
                    task.DueDate.Value < DateTime.Now);

            //Extension method-dal:
            //return dataProvider.GetAll()
            //    .Any(task => task.IsOverdue());        
        }

        public int GetCompletedTaskCount()
        {
            return dataProvider.GetAll()
                .Count(task => task.State == TaskState.Completed);
        }

        public List<ProjectTask> GetTasksByProjectId(int projectId)
        {
            return dataProvider.GetAll()
                .Where(task => task.ProjectId == projectId)
                .ToList();
        }

        //Önálló feladatok

        // Kiválasztja a nem befejezett, határidővel rendelkező feladatokat, amelyek határideje ma vagy a jövőben van
        public ProjectTask? GetNextTask()
        {
            return dataProvider.GetAll()
                .Where(task => task.State != TaskState.Completed &&
                    task.DueDate.HasValue &&
                    task.DueDate.Value.Date >= DateTime.Today)
                .OrderBy(task => task.DueDate)                  // Határidő szerint növekvő sorrendbe rendezi a feladatokat
                .FirstOrDefault();                             // Visszaadja a legközelebbi határidejű feladatot, vagy null-t, ha nincs ilyen
        }

        public List<ProjectTaskCountDto> GetTaskCountByProject()
        {
            return dataProvider.GetAll()
                .GroupBy(task => task.ProjectId)               // A feladatokat ProjectId alapján csoportosítja
                .Select(group => new ProjectTaskCountDto      // Minden csoportból létrehoz egy objektumot a projekt azonosítójával és a projekthez tartozó feladatok számával
                {
                    ProjectId = group.Key,
                    TaskCount = group.Count()
                })
                .ToList();
        }

        public List<TaskStateCountDto> GetTaskCountByState()
        {
            return dataProvider.GetAll()
                .GroupBy(task => task.State)                 // A feladatokat állapot alapján csoportosítja
                .Select(group => new TaskStateCountDto      // Minden állapothoz eltárolja az állapotot és a hozzá tartozó feladatok számát
                {
                    State = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(item => item.Count)  // A csoportokat a feladatok száma szerint csökkenő sorrendbe rendezi
                .ToList();
        }

        public List<PriorityStatisticsDto> GetPriorityStatistics()
        {
            return dataProvider.GetAll()
                .GroupBy(task => task.Priority)                                                    // A feladatokat prioritás alapján csoportosítja
                .Select(group => new PriorityStatisticsDto                                        // Minden prioritáshoz statisztikát készít
                {
                    Priority = group.Key,

                    TaskCount = group.Count(),                                                 // Megszámolja az adott prioritáshoz tartozó összes feladatot

                    CompletedCount = group.Count(task => task.State == TaskState.Completed)  // Megszámolja az adott prioritáshoz tartozó befejezett feladatokat
                })
                .ToList();
        }
        public List<ProjectStatisticsDto> GetProjectStatistics()
        {
            return dataProvider.GetAll()
                .GroupBy(task => task.ProjectId)                                                       // Csoportosítja a feladatokat ProjectId alapján
                .Select(group => new ProjectStatisticsDto
                {
                    ProjectId = group.Key,
                    TotalTaskCount = group.Count(),                                                 // Megszámolja a projekt összes feladatát
                    CompletedTaskCount = group.Count(task => task.State == TaskState.Completed),   // Megszámolja a projekt befejezett feladatait
                    ActiveTaskCount = group.Count(task => task.State != TaskState.Completed),     // Megszámolja a projekt még nem befejezett feladatait
                    OverdueTaskCount = group.Count(task => task.IsOverdue())                     // Az IsOverdue extension method segítségével megszámolja a lejárt feladatokat
                })
                .Where(project => project.ActiveTaskCount > 0)                                 // Csak azokat a projekteket tartja meg, amelyeknek van aktív feladatuk
                .OrderByDescending(project => project.OverdueTaskCount)                       // A projekteket a lejárt feladatok száma szerint csökkenő sorrendbe rendezi
                .ThenBy(project => project.ProjectId)                                        // Azonos számú lejárt feladat esetén ProjectId szerint növekvő sorrendbe rendezi
                .ToList();
        }
    }
}
