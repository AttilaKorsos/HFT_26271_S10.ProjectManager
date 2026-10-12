using HFT_26271_S10.ProjectManager.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.BusinessLogic.Interfaces
{
    public interface ITeamMemberService
    {
        List<TeamMember> GetAllTeamMembers();
        TeamMember? GetTeamMemberById(int id);
        bool CreateTeamMember(TeamMember teamMember);
        bool UpdateTeamMember(TeamMember teamMember);
        bool DeleteTeamMember(int id);
    }
}
