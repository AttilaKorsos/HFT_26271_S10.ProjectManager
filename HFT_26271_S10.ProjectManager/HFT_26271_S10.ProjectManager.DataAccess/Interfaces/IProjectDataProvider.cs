using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.DataAccess.Interfaces
{
    public interface IProjectDataProvider
    {
        List<Project> GetAll();
        Project? GetById(int id);
        void Add(Project project);
        bool Update(Project updatedProject);
        bool Delete(int id);
    }
}
