using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HFT_26271_S10.ProjectManager.DataAccess.DataProviders
{
    internal class ProjectJsonDataProvider
    {
        //3.1. feladat
        private static readonly string FilePath = Path.Combine("Data", "projects.json");
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        private List<Project> LoadProjects()
        {
            if (!File.Exists(FilePath)) return new List<Project>();

            try
            {
                string json = File.ReadAllText(FilePath);

                return JsonSerializer.Deserialize<List<Project>>(json, options) ?? new List<Project>();
            }
            catch (JsonException)
            {
                return new List<Project>();
            }
        }
        private void SaveTasks(List<Project> projects)
        {
            string json = JsonSerializer.Serialize(projects, options);

            File.WriteAllText(FilePath, json);
        }

        //3.2. feladat

        // READ - összes projektfeladat
        public List<Project> GetAll()
        {
            return LoadProjects();
        }

        // READ - keresés Id alapján
        public Project? GetById(int id)
        {
            List<Project> projects = GetAll();

            return projects.FirstOrDefault(project => project.Id == id);
        }

        // CREATE
        public void Add(Project project)
        {
            List<Project> projects = GetAll();

            projects.Add(project);

            SaveTasks(projects);
        }

        // UPDATE
        public bool Update(Project updatedProject)
        {
            List<Project> projects = GetAll();

            Project? existingProject = projects.FirstOrDefault(project => project.Id == updatedProject.Id);

            if (existingProject == null)
            {
                return false;
            }

            existingProject.Name = updatedProject.Name;
            existingProject.Description = updatedProject.Description;
            existingProject.Deadline = updatedProject.Deadline;
            existingProject.StartDate = updatedProject.StartDate;
            existingProject.Status = updatedProject.Status;

            SaveTasks(projects);

            return true;
        }

        // DELETE
        public bool Delete(int id)
        {
            List<Project> projects = GetAll();

            Project? projectToDelete = projects.FirstOrDefault(task => task.Id == id);

            if (projectToDelete == null) return false;

            projects.Remove(projectToDelete);

            SaveTasks(projects);

            return true;
        }
    }
}
