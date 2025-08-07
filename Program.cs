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
        public static void CheckCommandKeywords(ref string[] commandLineArguments)
        {
            foreach (string argument in commandLineArguments)
            {
                if (CommandKeywords.ContainsKey(argument))
                {
                    CommandKeywords[argument] = true;
                }
            }
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
            args = new string[] { "add", "list", "done", "blablalba", "bleblelbe" };
            Tracker.CheckCommandKeywords(ref args);
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
        }
    }
}
