using Task_Tracker_CLI;

public static class Tracker
{
    public static void Execute(ref string[] args, ref UserTaskList taskList)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Error: No commands were provided.");
            return;
        }
        switch (args[0])
        {
            case "add":
                if (!NumberOfArgumentsValidation(args.Count(), 2, 2)) { break; }
                if (!DescriptionValidation(args[1])) { break; }
                AddTask(args[1], ref taskList);
                break;

            case "update":
                if (!NumberOfArgumentsValidation(args.Count(), 3, 3)) { break; }
                if (!IdParsingValidation(args[1])) { break; }
                if (!DescriptionValidation(args[2])) { break; }
                UpdateTask(int.Parse(args[1]), args[2], ref taskList);
                break;
            case "delete":
                if (!NumberOfArgumentsValidation(args.Count(), 2, 2)) { break; }
                if (!IdParsingValidation(args[1])) { break; }
                DeleteTask(int.Parse(args[1]), ref taskList);
                break;
            case "mark-in-progress":
                if (!NumberOfArgumentsValidation(args.Count(), 2, 2)) { break; }
                if (!IdParsingValidation(args[1])) { break; }
                MarkTaskInProgress(int.Parse(args[1]), ref taskList);
                break;
            case "mark-done":
                if (!NumberOfArgumentsValidation(args.Count(), 2, 2)) { break; }
                if (!IdParsingValidation(args[1])) { break; }
                MarkTaskDone(int.Parse(args[1]), ref taskList);
                break;
            case "list":
                if (!NumberOfArgumentsValidation(args.Count(), 1, 2)) { break; }
                if (args.Count() == 2)
                {
                    switch (args[1])
                    {
                        case "done":
                            ListTasksByStatus(ref taskList, UserTaskStatus.Done);
                            break;
                        case "todo":
                            ListTasksByStatus(ref taskList, UserTaskStatus.ToDo);
                            break;
                        case "in-progress":
                            ListTasksByStatus(ref taskList, UserTaskStatus.InProgress);
                            break;
                        default:
                            Console.WriteLine("Error: Entered task status does not exist.");
                            break;
                    }
                }
                else
                {
                    ListAllTasks(ref taskList);
                }
                break;
            default:
                Console.WriteLine("Error: Entered command does not exist.");
                break;
        }
    }

    public static void AddTask(string description, ref UserTaskList taskList)
    {
        UserTask taskToAdd = new UserTask(description);
        taskList.Tasks.Add(taskToAdd);
        ListAffectedRow(ref taskList, taskToAdd);
    }
    public static void UpdateTask(int taskId, string description, ref UserTaskList taskList)
    {
        int? listId = FindListByTaskId(taskId, ref taskList);
        if (!TaskExists(listId))
        {
            return;
        }
        taskList.Tasks[(int)listId!].Description = description;
        taskList.Tasks[(int)listId!].UpdatedAt = DateTime.UtcNow.ToLocalTime();
        ListAffectedRow(ref taskList, taskList.Tasks[(int)listId!]);
    }
    public static void DeleteTask(int taskId, ref UserTaskList taskList)
    {
        int? listId = FindListByTaskId(taskId, ref taskList);
        if (!TaskExists(listId))
        {
            return;
        }
        taskList.Tasks.RemoveAt((int)listId!);
        Console.WriteLine("Task has been successfully deleted!");
    }
    public static void MarkTaskInProgress(int taskId, ref UserTaskList taskList)
    {
        int? listId = FindListByTaskId(taskId, ref taskList);
        if (!TaskExists(listId))
        {
            return;
        }
        taskList.Tasks[(int)listId!].Status = UserTaskStatus.InProgress;
        ListAffectedRow(ref taskList, taskList.Tasks[(int)listId!]);
    }
    public static void MarkTaskDone(int taskId, ref UserTaskList taskList)
    {
        int? listId = FindListByTaskId(taskId, ref taskList);
        if (!TaskExists(listId))
        {
            return;
        }
        taskList.Tasks[(int)listId!].Status = UserTaskStatus.Done;
        ListAffectedRow(ref taskList, taskList.Tasks[(int)listId!]);
    }
    public static void ListAllTasks(ref UserTaskList taskList)
    {
        Console.WriteLine();
        string header = ConsoleStyling.RowFormatting("Id",
                                         "Description",
                                         "Status",
                                         "CreatedAt",
                                         "UpdatedAt",
                                         4, ref taskList);
        int rowLength = header.Length;
        ConsoleStyling.PrintSpecialLine(rowLength);
        Console.WriteLine(header);
        ConsoleStyling.PrintLine(rowLength);
        foreach (var task in taskList.Tasks)
        {
            string row = ConsoleStyling.RowFormatting(task.Id.ToString(),
                                         task.Description,
                                         task.Status.ToString(),
                                         task.CreatedAt.ToString(),
                                         task.UpdatedAt.ToString(),
                                         4, ref taskList);
            Console.WriteLine(row);
        }
        ConsoleStyling.PrintSpecialLine(rowLength);
        Console.WriteLine();
    }
    public static void ListTasksByStatus(ref UserTaskList taskList, UserTaskStatus status)
    {
        string header = ConsoleStyling.RowFormatting("Id",
                                         "Description",
                                         "Status",
                                         "CreatedAt",
                                         "UpdatedAt",
                                         4, ref taskList);
        int rowLength = header.Length;
        Console.WriteLine();
        ConsoleStyling.PrintSpecialLine(rowLength);
        Console.WriteLine(header);
        ConsoleStyling.PrintLine(rowLength);
        foreach (var task in taskList.Tasks)
        {
            string row = ConsoleStyling.RowFormatting(task.Id.ToString(),
                                         task.Description,
                                         task.Status.ToString(),
                                         task.CreatedAt.ToString(),
                                         task.UpdatedAt.ToString(),
                                         4, ref taskList);
            if (task.Status == status)
            {
                Console.WriteLine(row);
            }
        }
        ConsoleStyling.PrintSpecialLine(rowLength);
        Console.WriteLine();
    }
    public static void ListAffectedRow(ref UserTaskList taskList, UserTask task)
    {
        string header = ConsoleStyling.RowFormatting("Id",
                                         "Description",
                                         "Status",
                                         "CreatedAt",
                                         "UpdatedAt",
                                         4, ref taskList);
        int rowLength = header.Length;
        Console.WriteLine();
        ConsoleStyling.PrintSpecialLine(rowLength);
        Console.WriteLine(header);
        ConsoleStyling.PrintLine(rowLength);
        string row = ConsoleStyling.RowFormatting(task.Id.ToString(),
                                        task.Description,
                                        task.Status.ToString(),
                                        task.CreatedAt.ToString(),
                                        task.UpdatedAt.ToString(),
                                        4, ref taskList);
        Console.WriteLine(row);
        ConsoleStyling.PrintSpecialLine(rowLength);
        Console.WriteLine();
    }
    public static int? FindListByTaskId(int taskId, ref UserTaskList taskList)
    {
        int listId = 0;
        foreach (UserTask task in taskList.Tasks)
        {
            if (task.Id == taskId)
            {
                return listId;
            }
            listId++;
        }
        return null;
    }
    public static bool TaskExists(int? taskId)
    {
        if (taskId == null)
        {
            Console.WriteLine("Error: Task with given id does not exist");
            return false;
        }
        return true;
    }

    //Console arguments validation

    public static bool IdParsingValidation(string id)
    {
        int result;
        bool success = int.TryParse(id, out result);
        if (!success)
        {
            Console.WriteLine("Error: Provided id does not exist.");
        }
        return success;
    }
    public static bool DescriptionValidation(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Error: No description was provided.");
            return false;
        }
        return true;
    }
    public static bool NumberOfArgumentsValidation(int numberOfArguments, int minNumberOfArguments, int maxNumberOfArguments)
    {
        if (numberOfArguments == 0)
        {
            Console.WriteLine("Error: No arguments were provided.");
            return false;
        }
        if (numberOfArguments > maxNumberOfArguments)
        {
            Console.WriteLine("Error: Too many arguments were provided.");
            return false;
        }
        if (numberOfArguments < maxNumberOfArguments && numberOfArguments < minNumberOfArguments)
        {
            Console.WriteLine("Error: Arguments are missing.");
            return false;
        }
        return true;
    }
}