using HFT_26271_S10.ProjectManager.Models.Enums;

namespace HFT_26271_S10.ProjectManager.Models.Classes
{
    public class TaskStateChangedEventArgs(ProjectTask task, TaskState oldState, TaskState newState, DateTime changedAt) : EventArgs
    {
        public ProjectTask Task { get; } = task;
        public TaskState OldState { get; } = oldState;
        public TaskState NewState { get; } = newState;
        public DateTime ChangedAt { get; } = changedAt;
    }
}