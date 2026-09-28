using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.Presentation.DTOs
{
    public class ProjectStatisticsDto
    {
        public int ProjectId { get; set; }
        public int TotalTaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
        public int ActiveTaskCount { get; set; }
        public int OverdueTaskCount { get; set; }
    }
}
