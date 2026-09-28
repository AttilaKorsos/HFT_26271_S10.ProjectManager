using HFT_26271_S10.ProjectManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HFT_26271_S10.ProjectManager.Models.Classes
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? Deadline { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
