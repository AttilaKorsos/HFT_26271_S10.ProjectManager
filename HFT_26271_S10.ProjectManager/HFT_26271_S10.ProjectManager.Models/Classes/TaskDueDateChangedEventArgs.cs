using System;
using System.Collections.Generic;
using System.Text;

namespace HFT_26271_S10.ProjectManager.Models.Classes
{
    public class TaskDueDateChangedEventArgs : EventArgs
    {
        public TaskDueDateChangedEventArgs(ProjectTask task, DateTime? oldDueDate, DateTime? newDueDate, DateTime changedAt)
        {
            Task = task;
            OldDueDate = oldDueDate;
            NewDueDate = newDueDate;
            ChangedAt = changedAt;
        }

        public ProjectTask Task { get; set; }
        public DateTime? OldDueDate { get; set; }
        public DateTime? NewDueDate { get; set; }
        public DateTime ChangedAt { get; set; }

    }
}
