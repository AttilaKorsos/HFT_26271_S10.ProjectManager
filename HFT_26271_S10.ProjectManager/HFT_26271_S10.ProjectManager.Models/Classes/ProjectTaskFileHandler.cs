using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HFT_26271_S10.ProjectManager.Models.Enums;

namespace HFT_26271_S10.ProjectManager.Models.Classes
{
    public class ProjectTaskFileHandler
    {
        public event Action? ProjectTasksSaved;
        public event Action<List<ProjectTask>> ProjectTasksLoaded;
        private string filePath;

        public ProjectTaskFileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public void SaveTasks(List<ProjectTask> projectTasks)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (ProjectTask task in projectTasks)
                {
                    string dueDate = task.DueDate.HasValue ? task.DueDate.Value.ToString("yyyy-mm-dd") : "";
                    writer.WriteLine($"{task.Id};{task.ProjectId};{task.Name};{task.Description};{dueDate};{task.Priority};{task.State}");
                    
                }
            }
            ProjectTasksSaved?.Invoke();
        }
        public List<ProjectTask> LoadTasks()
        {
            List<ProjectTask > tasks = new List<ProjectTask>();
            if (File.Exists(filePath)) return tasks;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    string[] data = line?.Split(';') ?? new string[0];
                    int id = int.Parse(data[0]);
                    int projectid = int.Parse(data[1]);
                    string name = data[2];
                    string description = data[3];
                    DateTime? duedate = data[4] != "" ? DateTime.Parse(data[4]) : null;
                    TaskPriority priority = Enum.Parse<TaskPriority>(data[5]);
                    TaskState state = Enum.Parse<TaskState>(data[6]);

                    ProjectTask task = new ProjectTask(id,projectid, name, description,duedate, priority, state);
                    tasks.Add(task);
                }
            }
            ProjectTasksLoaded?.Invoke(tasks);
            return tasks;
        }
        private static void ValidateText(string value, string fieldName)
        {
            if (value == null || value.Contains(';') || value.Contains('\r') || value.Contains('\n'))
                throw new ArgumentException($"'{fieldName}' could not be null, and could not contain semicolons or line breaks.");
        }
    }
}
