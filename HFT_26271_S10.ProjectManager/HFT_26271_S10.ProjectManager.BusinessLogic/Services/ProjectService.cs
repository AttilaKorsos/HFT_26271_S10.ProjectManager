using HFT_26271_S10.ProjectManager.DataAccess.DataProviders;
using HFT_26271_S10.ProjectManager.Models.Classes;
using HFT_26271_S10.ProjectManager.Models.DTOs;
using HFT_26271_S10.ProjectManager.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HFT_26271_S10.ProjectManager.BusinessLogic.Services
{
    internal class ProjectService
    {
        private readonly ProjectJsonDataProvider dataProvider;

        public ProjectService(ProjectJsonDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public List<Project> GetAllProjects()
        {
            return dataProvider.GetAll();
        }

        public Project? GetProjectById(int id)
        {
            return dataProvider.GetById(id);
        }

        public bool CreateProject(Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                return false;

            dataProvider.Add(project);
            return true;
        }

        public bool UpdateProject(Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                return false;

            return dataProvider.Update(project);
        }

        public bool DeleteProject(int id)
        {
            return dataProvider.Delete(id);
        }
    }
}
