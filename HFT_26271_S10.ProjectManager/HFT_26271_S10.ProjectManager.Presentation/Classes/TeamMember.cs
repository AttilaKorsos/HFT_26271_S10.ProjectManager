using HFT_26271_S10.ProjectManager.Presentation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HFT_26271_S10.ProjectManager.Presentation.Classes
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TeamMemberRole Role { get; set; }
    }
}
