using HFT_26271_S10.ProjectManager.Presentation.Classes;
using HFT_26271_S10.ProjectManager.Presentation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFT_26271_S10.ProjectManager.Presentation.Extensions
{
    public static class ProjectTaskExtensions
    {
        public static bool IsOverdue(this ProjectTask task)
        {
            return task.State != TaskState.Completed
                && task.DueDate.HasValue
                && task.DueDate.Value.Date < DateTime.Today;
        }
    }
}
