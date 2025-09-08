using System.ComponentModel;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Task_Tracker_CLI
{
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

            UserTaskList taskList = new UserTaskList();
            string tasksFileName = "tasks.json";

            if (!File.Exists(tasksFileName))
            {
                File.Create(tasksFileName);
            }

            string tasksJsonString = File.ReadAllText(tasksFileName);


            if (!string.IsNullOrWhiteSpace(tasksJsonString))
            {
                taskList = JsonSerializer.Deserialize<UserTaskList>(tasksJsonString, options)!;
            }


            if (taskList.Tasks.Count > 0)
            {
                UserTask.Count = taskList.Tasks.Last().Id + 1;
            }

            //args = new string[] { "list", "done" };

            Tracker.Execute(ref args, ref taskList);
            string jsonString = JsonSerializer.Serialize(taskList, options);
            File.WriteAllText(tasksFileName, jsonString);
        }
    }
}
