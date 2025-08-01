using System.ComponentModel;

namespace Task_Tracker_CLI
{
    internal class Program
    {
        public enum TaskStatus
        {
            ToDo,
            InProgress,
            Done
        }
        public class Task
        {
            private readonly uint _id;
            private string _description;
            private TaskStatus _status;
            private readonly DateTime _createdAt;
            private DateTime _updatedAt;

            public uint Id 
            { 
                get { return _id; } 
            }
            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }
            public TaskStatus Status
            {
                get { return _status; }
                set { _status = value; }
            }
            public DateTime CreatedAt
            {
                get { return _createdAt; }
            }
            public DateTime UpdatedAt
            {
                get { return _updatedAt; }
                set { _updatedAt = value; }
            }

            public static uint Count = 0;
            
            public Task(string description = "")
            {
                _id = Count;
                _description = description;
                _status = TaskStatus.ToDo;
                _createdAt = DateTime.Now;

                ++Count;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            foreach (var arg in args)
            {
                Console.WriteLine(arg.GetType());
            }
        }
    }
}
