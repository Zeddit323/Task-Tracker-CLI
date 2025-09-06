using System.ComponentModel;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Task_Tracker_CLI
{
    public enum TaskStatus
    {
        ToDo, InProgress, Done
    }
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static int Count = 0;

        public Task(string description = "")
        {
            Id = Count;
            Description = description;
            Status = TaskStatus.ToDo;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            ++Count;
        }
    }
    public class TasksList
    {
        public List<Task> Tasks { get; set; }
        public TasksList()
        {
            Tasks = new List<Task>();
        }
        public void AddTask(string description)
        {
            Tasks.Add(new Task(description));
        }
        public void UpdateTask(int taskId, string description)
        {
            int? listId = FindListByTaskId(taskId);
            if (!TaskExists(listId))
            {
                return;
            }
            Tasks[(int)listId!].Description = description;
            Tasks[(int)listId!].UpdatedAt = DateTime.UtcNow;
        }
        public void DeleteTask(int taskId)
        {
            int? listId = FindListByTaskId(taskId);
            if (!TaskExists(listId))
            {
                return;
            }
            Tasks.RemoveAt((int)listId!);
        }
        public void MarkTaskInProgress(int id)
        {
            
        }
        public void MarkTaskDone(int id)
        {

        }
        public void ListAllTasks(ref TasksList taskList)
        {
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
        }
        public void ListTasksByStatus(TaskStatus status)
        {

        }
        public int? FindListByTaskId(int taskId)
        {
            int listId = 0;
            foreach (Task task in Tasks)
            {
                if (task.Id == taskId)
                {
                    return listId;
                }
                listId++;
            }
            return null;
        }
        public bool TaskExists(int? taskId)
        {
            if (taskId == null)
            {
                Console.WriteLine("Error: Task with given id does not exist");
                return false;
            }
            return true;
        }
    }

    public static class Tracker
    {
        public static void Execute(ref string[] args, ref TasksList taskList)
        {
            if( args.Length == 0)
            {
                Console.WriteLine("Error: No commands were provided.");
                return;
            }
            switch (args[0])
            {
                case "add":
                    if (!NumberOfArgumentsValidation(args.Count(), 2)) { break; }
                    if (!DescriptionValidation(args[1])) { break; }

                    taskList.AddTask(args[1]);


                    break;

                case "update":
                    if (!NumberOfArgumentsValidation(args.Count(), 3)) {  break; }
                    if (!IdParsingValidation(args[1])) { break; }
                    if (!DescriptionValidation(args[2])) { break; }
                    taskList.UpdateTask(int.Parse(args[1]), args[2]);
                    break;
                case "delete":
                    if (!NumberOfArgumentsValidation(args.Count(), 2)) { break; }
                    if (!IdParsingValidation(args[1])) { break; }
                    taskList.DeleteTask(int.Parse(args[1]));
                    break;
                case "list":
                    if (!NumberOfArgumentsValidation(args.Count(), 1)) { break; }
                    taskList.ListAllTasks(ref taskList);
                    break;
                default:
                    Console.WriteLine("Error: Entered command does not exist.");
                    break;
            }            
            
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
        public static bool NumberOfArgumentsValidation(int numberOfArguments, int maxNumberOfArguments)
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
            if (numberOfArguments < maxNumberOfArguments)
            {
                Console.WriteLine("Error: Arguments are missing.");
                return false;
            }
            return true;
        }
    }
    public static class ConsoleStyling
    {
        public static string CenterString(string text, int width)
        {
            if (text.Length >= width)
            {
                return text;
            }
            int leftPadding = (width - text.Length) / 2;
            int rightPadding = width - text.Length - leftPadding;

            return new string(' ', leftPadding) + text + 
                new string(' ', rightPadding);
        }
        public static string RowFormatting(
            string id, 
            string description,
            string status,
            string createdAt,
            string updatedAt,
            int widthBetweenRows,
            ref TasksList taskList)
        {
            int idLength = GetLongestPropertyLength("id", ref taskList);
            int descriptionLength = GetLongestPropertyLength("description", ref taskList);
            int statusLength = GetLongestPropertyLength("status", ref taskList);
            int createdAtLength = GetLongestPropertyLength("createdAt", ref taskList);
            int updatedAtLength = GetLongestPropertyLength("updatedAt", ref taskList);

            string formattedString = 
                $"||{CenterString(id, widthBetweenRows + idLength)}|" +
                $"|{CenterString(description, widthBetweenRows + descriptionLength)}|" +
                $"|{CenterString(status, widthBetweenRows + statusLength)}|" +
                $"|{CenterString(createdAt, widthBetweenRows + createdAtLength)}|" +
                $"|{CenterString(updatedAt, widthBetweenRows + updatedAtLength)}||";
            return formattedString;
        }
        public static void PrintLine(int rowLength)
        {
            Console.WriteLine("||" + new string('=', rowLength - 4) + "||");
        }
        public static void PrintSpecialLine(int rowLength)
        {
            Console.WriteLine("(]" + new string('=', rowLength - 4) + "[)");
        }
        public static int GetLongestPropertyLength(string propertyName, ref TasksList taskList)
        {
            int longestPropertyLength = 0;
            switch (propertyName)
            {
                case "id":
                case "Id":
                    longestPropertyLength = taskList.Tasks.Max(task => Convert.ToString(task.Id).Length);
                    if (longestPropertyLength < "id".Length)
                    {
                        longestPropertyLength = "id".Length;
                    }
                    break;
                case "description":
                case "Description":
                    longestPropertyLength = taskList.Tasks.Max(task => task.Description.Length);
                    if (longestPropertyLength < "description".Length)
                    {
                        longestPropertyLength = "description".Length;
                    }
                    break;
                case "status":
                case "Status":
                    longestPropertyLength = taskList.Tasks.Max(task => task.Status.ToString().Length);
                    if (longestPropertyLength < "status".Length)
                    {
                        longestPropertyLength = "status".Length;
                    }
                    break;
                case "createdAt":
                case "CreatedAt":
                    longestPropertyLength = taskList.Tasks.Max(task => task.CreatedAt.ToString().Length);
                    if (longestPropertyLength < "createdAt".Length)
                    {
                        longestPropertyLength = "createdAt".Length;
                    }
                    break;
                case "updatedAt":
                case "UpdatedAt":
                    longestPropertyLength = taskList.Tasks.Max(task => task.UpdatedAt.ToString().Length);
                    if (longestPropertyLength < "updatedAt".Length)
                    {
                        longestPropertyLength = "updatedAt".Length;
                    }
                    break;
                default:
                    return longestPropertyLength;
            }

            return longestPropertyLength;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower)
                }
            };

            TasksList taskList = new TasksList();
            string tasksFileName = "tasks.json";

            if (!File.Exists(tasksFileName))
            {
                File.Create(tasksFileName);
            }

            string tasksJsonString = File.ReadAllText(tasksFileName);


            if (!string.IsNullOrWhiteSpace(tasksJsonString))
            {
                taskList = JsonSerializer.Deserialize<TasksList>(tasksJsonString, options)!;
            }


            if (taskList.Tasks.Count > 0)
            {
                Task.Count = taskList.Tasks.Last().Id + 1;
            }

            args = new string[] { "list" };

            Tracker.Execute(ref args, ref taskList);
            string jsonString = JsonSerializer.Serialize(taskList, options);
            File.WriteAllText(tasksFileName, jsonString);
        }
    }
}
