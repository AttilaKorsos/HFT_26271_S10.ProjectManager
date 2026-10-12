using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.DataAccess.Interfaces
{
    public interface IProjectTaskDataProvider
    {
        List<ProjectTask> GetAll();
        ProjectTask? GetById(int id);
        void Add(ProjectTask task);
        bool Update(ProjectTask updatedTask);
        bool Delete(int id);
    }
}
