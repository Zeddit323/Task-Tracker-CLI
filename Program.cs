using System.ComponentModel;
using System.Text.Json;

namespace Task_Tracker_CLI
{
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
    public class Task
    {
        public uint Id { get; }
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; set; }

        public static uint Count = 0;

        public Task(string description = "")
        {
            Id = Count;
            Description = description;
            Status = TaskStatus.ToDo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

            ++Count;
        }
    }

    public static class Tracker
    {
        public static Dictionary<string, bool> CommandKeywords =
            new Dictionary<string, bool>()
            {
                { "add", false },
                { "update", false },
                { "delete", false },
                { "mark-in-progress", false },
                { "mark-done", false },
                { "list", false },
                { "done", false },
                { "todo", false },
                { "in-progress", false }
            };
        public static void Execute(ref string[] args)
        {
            // Removing command keywords from the argument list, leaving only non-keyword arguments for Tracker functions
            
            List<string> commandLineArgumentsList = args.ToList();
            int keywordArgumentsCount = 0;

            foreach (string argument in args)
            {
                if (CommandKeywords.ContainsKey(argument))
                {
                    CommandKeywords[argument] = true;
                    commandLineArgumentsList.Remove(argument);

                    keywordArgumentsCount++;
                }
            }

            // Validating if provided command exists and is correct

            if (keywordArgumentsCount == 0)
            {
                Console.WriteLine("No command has been entered.");
                return;
            }
            else if(keywordArgumentsCount <= 2)
            {
                if (CommandKeywords["list"] == false && keywordArgumentsCount > 1)
                {
                    Console.WriteLine("Only one command must be entered.");
                    return;
                }
                else if (!CommandKeywords["done"]
                    && !CommandKeywords["todo"]
                    && !CommandKeywords["in-progress"]
                    && CommandKeywords["list"]
                    && keywordArgumentsCount > 1)
                {
                    Console.WriteLine("Only one command must be entered.");
                    return;
                }
                else if(CommandKeywords["done"]
                    || CommandKeywords["todo"]
                    || CommandKeywords["in-progress"]
                    && keywordArgumentsCount == 1)
                {
                    Console.WriteLine("Command cannot be executed by itself");
                    return;
                }
            }
            foreach (string argument in commandLineArgumentsList)
            {
                Console.WriteLine(argument);
            }
            Console.WriteLine(keywordArgumentsCount);
        }
        public static void ResetCommandKeywords()
        {
            foreach (KeyValuePair<string, bool> kvp in Tracker.CommandKeywords)
            {
                CommandKeywords[kvp.Key] = false;
            }
        }
        public static void AddTask(string description)
        {

        }
        public static void UpdateTask(uint id, string description)
        {

        }
        public static void DeleteTask(uint id)
        {

        }
        public static void MarkTaskInProgress(uint id)
        {

        }
        public static void MarkTaskDone(uint id)
        {

        }
        public static void ListAllTasks()
        {

        }
        public static void ListTasksByStatus(TaskStatus status)
        {

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { "add", "blablalba", "bleblelbe" };
            Tracker.Execute(ref args);
            Console.WriteLine();
            foreach (KeyValuePair<string, bool> kvp in Tracker.CommandKeywords)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }
            Console.WriteLine();
            Tracker.ResetCommandKeywords();
            foreach (KeyValuePair<string, bool> kvp in Tracker.CommandKeywords)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }
            Console.WriteLine();
        }
    }
}
