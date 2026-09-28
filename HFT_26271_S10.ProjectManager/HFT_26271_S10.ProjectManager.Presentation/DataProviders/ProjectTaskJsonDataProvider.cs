using HFT_26271_S10.ProjectManager.Presentation.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HFT_26271_S10.ProjectManager.Presentation.DataProviders
{
    public class ProjectTaskJsonDataProvider
    {
        private static readonly string FilePath = Path.Combine("Data", "projecttasks.json");
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        private List<ProjectTask> LoadTasks()
        {
            if (!File.Exists(FilePath)) return new List<ProjectTask>();

            try
            {
                string json = File.ReadAllText(FilePath);

                return JsonSerializer.Deserialize<List<ProjectTask>>(json, options) ?? new List<ProjectTask>();
            }
            catch (JsonException)
            {
                return new List<ProjectTask>();
            }
        }
        private void SaveTasks(List<ProjectTask> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, options);

            File.WriteAllText(FilePath, json);
        }

        //3.2. feladat

        // READ - összes projektfeladat
        public List<ProjectTask> GetAll()
        {
            return LoadTasks();
        }

        // READ - keresés Id alapján
        public ProjectTask? GetById(int id)
        {
            List<ProjectTask> tasks = GetAll();

            return tasks.FirstOrDefault(task => task.Id == id);
        }

        // CREATE
        public void Add(ProjectTask task)
        {
            List<ProjectTask> tasks = GetAll();

            tasks.Add(task);

            SaveTasks(tasks);
        }

        // UPDATE
        public bool Update(ProjectTask updatedTask)
        {
            List<ProjectTask> tasks = GetAll();

            ProjectTask? existingTask = tasks.FirstOrDefault(task => task.Id == updatedTask.Id);

            if (existingTask == null)
            {
                return false;
            }

            existingTask.ProjectId = updatedTask.ProjectId;
            existingTask.Name = updatedTask.Name;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.Priority = updatedTask.Priority;
            existingTask.State = updatedTask.State;

            SaveTasks(tasks);

            return true;
        }

        // DELETE
        public bool Delete(int id)
        {
            List<ProjectTask> tasks = GetAll();

            ProjectTask? taskToDelete = tasks.FirstOrDefault(task => task.Id == id);

            if (taskToDelete == null) return false;

            tasks.Remove(taskToDelete);

            SaveTasks(tasks);

            return true;
        }
    }
}
