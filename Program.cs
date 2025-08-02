using System.ComponentModel;

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

        }
    }
}
