using Task_Tracker_CLI;

// Handles displaying tasks in a table format
public static class ConsoleStyling
{
    public static string CenterTextInCell(string text, int width)
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
        ref UserTaskList taskList)
    {
        int idLength = GetLongestPropertyLength("id", ref taskList);
        int descriptionLength = GetLongestPropertyLength("description", ref taskList);
        int statusLength = GetLongestPropertyLength("status", ref taskList);
        int createdAtLength = GetLongestPropertyLength("createdAt", ref taskList);
        int updatedAtLength = GetLongestPropertyLength("updatedAt", ref taskList);

        string formattedString =
            $"||{CenterTextInCell(id, widthBetweenRows + idLength)}|" +
            $"|{CenterTextInCell(description, widthBetweenRows + descriptionLength)}|" +
            $"|{CenterTextInCell(status, widthBetweenRows + statusLength)}|" +
            $"|{CenterTextInCell(createdAt, widthBetweenRows + createdAtLength)}|" +
            $"|{CenterTextInCell(updatedAt, widthBetweenRows + updatedAtLength)}||";
        return formattedString;
    }
    // Used as a separator
    public static void PrintLine(int rowLength)
    {
        Console.WriteLine("||" + new string('=', rowLength - 4) + "||");
    }
    // Serves as both the header and footer line
    public static void PrintSpecialLine(int rowLength)
    {
        Console.WriteLine("(]" + new string('=', rowLength - 4) + "[)");
    }
    // Ensures that each row is formatted to have the same length
    public static int GetLongestPropertyLength(string propertyName, ref UserTaskList taskList)
    {
        int longestPropertyLength = 0;
        switch (propertyName)
        {
            case "id":
                longestPropertyLength = taskList.Tasks.Max(task => Convert.ToString(task.Id).Length);
                if (longestPropertyLength < "id".Length)
                {
                    longestPropertyLength = "id".Length;
                }
                break;
            case "description":
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
                longestPropertyLength = taskList.Tasks.Max(task => task.CreatedAt.ToString().Length);
                if (longestPropertyLength < "createdAt".Length)
                {
                    longestPropertyLength = "createdAt".Length;
                }
                break;
            case "updatedAt":
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