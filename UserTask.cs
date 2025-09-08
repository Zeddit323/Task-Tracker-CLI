using Task_Tracker_CLI;

public class UserTask
{
    public int Id { get; set; }
    public string Description { get; set; }
    public UserTaskStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static int Count = 0;

    public UserTask(string description = "")
    {
        Id = Count;
        Description = description;
        Status = UserTaskStatus.ToDo;
        CreatedAt = DateTime.UtcNow.ToLocalTime();
        UpdatedAt = DateTime.UtcNow.ToLocalTime();

        ++Count;
    }
}