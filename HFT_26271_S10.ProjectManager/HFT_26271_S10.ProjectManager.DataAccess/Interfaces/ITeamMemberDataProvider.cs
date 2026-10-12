using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.DataAccess.Interfaces
{
    public interface ITeamMemberDataProvider
    {
        List<TeamMember> GetAll();
        TeamMember? GetById(int id);
        void Add(TeamMember teamMember);
        bool Update(TeamMember updatedteamMember);
        bool Delete(int id);
    }
}
