using HFT_26271_S10.ProjectManager.Presentation.Classes;
using HFT_26271_S10.ProjectManager.Presentation.DataProviders;
using HFT_26271_S10.ProjectManager.Presentation.DTOs;
using HFT_26271_S10.ProjectManager.Presentation.Enums;

namespace HFT_26271_T00.ProjectManager.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============|1.3.|============");
            ProjectTask task = new ProjectTask(1, 1, "Task 1", "Description 1", DateTime.Now, TaskPriority.High, TaskState.NotStarted);
            ProjectTask task1 = new ProjectTask(2, 1, "Task 1", "Description 1", DateTime.Now, TaskPriority.High, TaskState.NotStarted);
            ProjectTask task2 = new ProjectTask(3, 1, "Task 1", "Description 1", DateTime.Now, TaskPriority.High, TaskState.NotStarted);
            List<ProjectTask> tasks = new List<ProjectTask> { task, task1, task2 };
            task.StateChanged += Task_StateChanged;
            task.State = TaskState.InProgress;

            Console.WriteLine("============|1.4.|============");
            ProjectTask T1 = new ProjectTask(1, 1, "Task 1", "Description 1", DateTime.Today, TaskPriority.High, TaskState.NotStarted);
            T1.DueDateChanged += Task_DueDateChanged;
            T1.DueDate = DateTime.Now;

            Console.WriteLine("============|2.1.|============");
            string path = Path.Combine(Environment.CurrentDirectory, "Data");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Data directory created");
            }

            string projecttaskspath = Path.Combine(path, "projecttasks.txt");
            if (!File.Exists(projecttaskspath))
            {
                File.Create(projecttaskspath);
                Console.WriteLine("projecttasks.txt created");
            }
            string events = Path.Combine(path, "events.log");
            if (!File.Exists(events))
            {
                File.Create(events);
                Console.WriteLine("events.log created");
            }

            Console.WriteLine("============|2.2.|============");
            ProjectTaskFileHandler handler = new ProjectTaskFileHandler(projecttaskspath);
            handler.ProjectTasksSaved += () => Console.WriteLine("[SAVE] Tasks Saved");
            handler.SaveTasks(tasks);

            Console.WriteLine("============|2.3.|============");
            handler.ProjectTasksLoaded += Handler_ProjectTasksLoaded;

            Console.WriteLine("\n==========|2.4.|==========\n");
            string[] eventsLogFile = File.ReadAllLines(events);
            Console.WriteLine("Events log:");
            foreach (string line in eventsLogFile)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("\n==========|3.3.|==========\n");
            ProjectTaskJsonDataProvider dataProvider = new();
            ProjectTaskService taskService = new(dataProvider);

            if (taskService.GetAllTasks().Count == 0)
            {
                taskService.CreateTask(new ProjectTask(1, 1, "Task 1", "Description 1", DateTime.Today.AddDays(5), TaskPriority.High, TaskState.NotStarted));
                taskService.CreateTask(new ProjectTask(2, 1, "Task 2", "Description 2", DateTime.Today.AddDays(10), TaskPriority.Medium, TaskState.InProgress));
                taskService.CreateTask(new ProjectTask(3, 1, "Task 3", "Description 3", null, TaskPriority.Low, TaskState.NotStarted));
                taskService.CreateTask(new ProjectTask(4, 2, "Task 4", "Description 4", DateTime.Today.AddDays(-5), TaskPriority.High, TaskState.InProgress));
                taskService.CreateTask(new ProjectTask(5, 2, "Task 5", "Description 5", null, TaskPriority.Medium, TaskState.Completed));
                taskService.CreateTask(new ProjectTask(6, 2, "Task 6", "Description 6", DateTime.Today.AddDays(15), TaskPriority.Low, TaskState.Completed));
                Console.WriteLine("ProjectTasks added manually.");
            }
            List<ProjectTask> jsonTasks = taskService.GetAllTasks();
            Handler_ProjectTasksLoaded(jsonTasks);

            Console.WriteLine("\n==========|3.4.|==========\n");

            // 1. High vagy Critical prioritású feladatok
            Console.WriteLine("High or Critical priority tasks:");
            List<ProjectTask> highOrCriticalTasks = taskService.GetHighOrCriticalPriorityTasks();
            foreach (var task in highOrCriticalTasks)
            {
                Console.WriteLine(task);
            }

            // 2. InProgress feladatok név szerint rendezve
            Console.WriteLine("\nIn progress tasks ordered by name:");
            List<ProjectTask> inProgressTasks = taskService.GetInProgressTasksOrderedByName();
            foreach (var task in inProgressTasks)
            {
                Console.WriteLine(task);
            }

            // 3. Projektfeladatok nevei
            Console.WriteLine("\nProject task names:");
            List<string> taskNames = taskService.GetProjectTaskNames();

            foreach (string name in taskNames)
            {
                Console.WriteLine(name);
            }

            //Rövidebben:
            //taskNames.ForEach(task => Console.WriteLine(task));


            // 4. Van-e lejárt, be nem fejezett feladat           
            bool hasOverdueTasks = taskService.HasOverdueTasks();
            Console.WriteLine(hasOverdueTasks ? "There is at least one overdue task." : "There are no overdue tasks.");

            // 5. Completed feladatok száma
            Console.Write("\nNumber of completed tasks: ");
            int completedTaskCount = taskService.GetCompletedTaskCount();
            Console.WriteLine(completedTaskCount);


            // 6. Adott projekthez tartozó feladatok
            int projectId = 1;

            Console.WriteLine($"\nTasks of project {projectId}:");
            List<ProjectTask> projectTasks = taskService.GetTasksByProjectId(projectId);

            foreach (ProjectTask task in projectTasks)
            {
                Console.WriteLine(task);
            }

            Console.WriteLine("\n==========|Önálló LINQ feladatok|==========\n");
            // 1. Legközelebbi határidő
            Console.WriteLine("Next task:");

            ProjectTask? nextTask = taskService.GetNextTask();

            if (nextTask != null)
            {
                Console.WriteLine(nextTask);
            }
            else
            {
                Console.WriteLine("There are no upcoming tasks.");
            }


            // 2. Projektfeladatok száma projektenként
            Console.WriteLine("\nTask count by project:");

            List<ProjectTaskCountDto> taskCountByProject = taskService.GetTaskCountByProject();

            foreach (ProjectTaskCountDto item in taskCountByProject)
            {
                Console.WriteLine($"Project ID: {item.ProjectId}, Task count: {item.TaskCount}");
            }


            // 3. Feladatok száma állapotonként
            Console.WriteLine("\nTask count by state:");

            List<TaskStateCountDto> taskCountByState = taskService.GetTaskCountByState();

            foreach (TaskStateCountDto item in taskCountByState)
            {
                Console.WriteLine($"State: {item.State}, Task count: {item.Count}");
            }


            // 4. Prioritási statisztika
            Console.WriteLine("\nPriority statistics:");

            List<PriorityStatisticsDto> priorityStatistics = taskService.GetPriorityStatistics();

            foreach (PriorityStatisticsDto item in priorityStatistics)
            {
                Console.WriteLine(
                    $"Priority: {item.Priority}, " +
                    $"Task count: {item.TaskCount}, " +
                    $"Completed: {item.CompletedCount}");
            }


            // 5. Projektenkénti összesítés
            Console.WriteLine("\nProject statistics:");

            List<ProjectStatisticsDto> projectStatistics = taskService.GetProjectStatistics();

            foreach (ProjectStatisticsDto item in projectStatistics)
            {
                Console.WriteLine(
                    $"Project ID: {item.ProjectId}, " +
                    $"Total: {item.TotalTaskCount}, " +
                    $"Completed: {item.CompletedTaskCount}, " +
                    $"Active: {item.ActiveTaskCount}, " +
                    $"Overdue: {item.OverdueTaskCount}");
            }

        }
        private static void Handler_ProjectTasksLoaded(List<ProjectTask> tasks)
        {
            Console.WriteLine("[TASKS LOADED]:");
            foreach (ProjectTask task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        private static void Task_DueDateChanged(object sender, TaskDueDateChangedEventArgs e)
        {
            Console.WriteLine($"[DUE DATE CHANGED]: {e.OldDueDate} -> {e.NewDueDate}");

        }
        private static void Task_StateChanged(object sender, TaskStateChangedEventArgs e)
        {
            Console.WriteLine($"[STATE CHANGED]: {e.OldState} -> {e.NewState}");
        }

    }
}
