using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HFT_26271_S10.ProjectManager.Presentation.Enums;

namespace HFT_26271_S10.ProjectManager.Presentation.Classes
{
    public delegate void TaskStateChangedHandler(object sender, TaskStateChangedEventArgs e);
    public delegate void TaskDueDateChangedHandler(object sender,TaskDueDateChangedEventArgs e);
    public class ProjectTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private DateTime? duedate;
        public TaskPriority Priority { get; set; }
        private TaskState state;

        public ProjectTask(int id, int projectId, string name, string description, DateTime? duedate, TaskPriority priority, TaskState state)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Description = description;
            DueDate = duedate;
            Priority = priority;
            State = state;
        }

        public TaskState State
        {
            get => state;
            set
            {
                if (value != state)
                {
                    TaskState oldState = state;
                    state = value;
                    StateChanged?.Invoke(this, new TaskStateChangedEventArgs(this, oldState, value, DateTime.Now));
                }
            }
        }
        public DateTime? DueDate
        {
            get => duedate;
            set
            {
                if (value != duedate)
                {
                    DateTime? oldState = duedate;
                    duedate = value;
                    DueDateChanged?.Invoke(this, new TaskDueDateChangedEventArgs(this,oldState,value,DateTime.Now));
                }
            }
        }


        public event TaskStateChangedHandler? StateChanged;
        public event TaskDueDateChangedHandler? DueDateChanged;

        public override string ToString()
        {
            return $"Task ID: {Id}, Name: {Name}, Description: {Description}, Due Date: {DueDate}, Priority: {Priority}, State: {State}";
        }

    }
}
