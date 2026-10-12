using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.BusinessLogic.Interfaces
{
    public interface IProjectService
    {
        List<Project> GetAllProjects();
        Project? GetProjectById(int id);
        bool CreateProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(int id);
    }
}
