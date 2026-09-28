using HFT_26271_S10.ProjectManager.Presentation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.Presentation.DTOs
{
    public class PriorityStatisticsDto
    {
        public TaskPriority Priority { get; set; }
        public int TaskCount { get; set; }
        public int CompletedCount { get; set; }
    }
}
