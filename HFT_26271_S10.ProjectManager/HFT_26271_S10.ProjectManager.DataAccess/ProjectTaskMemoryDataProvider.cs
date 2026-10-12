using HFT_26271_S10.ProjectManager.DataAccess.Interfaces;
using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.DataAccess
{
    public class ProjectTaskMemoryDataProvider : IProjectTaskDataProvider
    {
        List<ProjectTask> tasks = new List<ProjectTask>();
        public void Add(ProjectTask task)
        {
            tasks.Add(task);
        }

        public bool Delete(int id)
        {
            ProjectTask? TasktoRemove = GetById(id);
            if (TasktoRemove != null) return false;

            tasks.Remove(TasktoRemove);
            return true;
        }

        public List<ProjectTask> GetAll()
        {
            return tasks;
        }

        public ProjectTask? GetById(int id)
        {
            return tasks.FirstOrDefault(task => task.Id == id);
        }

        public bool Update(ProjectTask updatedTask)
        {

            ProjectTask? existingTask = tasks.FirstOrDefault(task => task.Id == updatedTask.Id);

            if (existingTask == null)
            {
                return false;
            }

            PropertyInfo[] propertyInfos = existingTask.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                propertyInfo.SetValue(existingTask,propertyInfo.GetValue(updatedTask));
            }
            return true;
        }
    }
}
