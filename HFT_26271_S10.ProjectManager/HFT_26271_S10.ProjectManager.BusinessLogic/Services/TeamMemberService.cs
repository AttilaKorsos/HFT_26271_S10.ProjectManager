using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HFT_26271_S10.ProjectManager.Models.Classes;
using HFT_26271_S10.ProjectManager.DataAccess.DataProviders;
using HFT_26271_S10.ProjectManager.Models.DTOs;
using HFT_26271_S10.ProjectManager.Models.Enums;
using HFT_26271_S10.ProjectManager.Models.Extensions;

namespace HFT_26271_S10.ProjectManager.BusinessLogic.Services
{
    internal class TeamMemberService
    {
        private readonly TeamMemberJsonDataProvider dataProvider;

        public TeamMemberService(TeamMemberJsonDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public List<TeamMember> GetAllTeamMembers()
        {
            return dataProvider.GetAll();
        }

        public TeamMember? GetTeamMemberById(int id)
        {
            return dataProvider.GetById(id);
        }

        public bool CreateTeamMember(TeamMember teamMember)
        {
            if (string.IsNullOrWhiteSpace(teamMember.Name))
                return false;

            dataProvider.Add(teamMember);
            return true;
        }

        public bool UpdateTeamMember(TeamMember teamMember)
        {
            if (string.IsNullOrWhiteSpace(teamMember.Name))
                return false;

            return dataProvider.Update(teamMember);
        }

        public bool DeleteTeamMember(int id)
        {
            return dataProvider.Delete(id);
        }
    }
}
