using Task_Tracker_CLI;

public class UserTaskList
{
    public List<UserTask> Tasks { get; set; }
    public UserTaskList()
    {
        Tasks = new List<UserTask>();
    }
}