# Task Tracker CLI
A simple command-line app written in C# that helps you keep track of your duties. <br/>
Your tasks are stored locally in `task.json` file. <br/>
This project is part of the [Task Tracker CLI](https://roadmap.sh/projects/task-tracker) challenge on [roadmap.sh](https://roadmap.sh).
## Features
- **Adding** a task with a description
- **Updating** a description of an existing task
- **Deleting** a task
- **Changing status** of a task to `in-progress` or `done` (by default task's status is `todo`)
- **Listing** all tasks with the ability to filter them by status (`todo`, `in-progress`, `done`)
## Instalation
> [!IMPORTANT]
> To build this project, you need to install the <a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0">.NET 8.0 SDK</a>.
1. Clone this repository
```bash
git clone https://github.com/Zeddit323/Task-Tracker-CLI.git
```
2. Navigate to the project directory
```bash
cd Task-Tracker-CLI
```
3. Restore dependencies
```bash
dotnet restore
```
4. Build the project
```bash
dotnet build
```
## Usage
Use `help` command to get the list of available commands:
```bash
dotnet run -- help
```
Output:
```
List of commands:

  help                                 - displays list of commands
  add "{description}"                  - creates a new task to track
  update {task-id} "{description}"     - updates a task
  delete {task-id}                     - removes a task
  mark-in-progress {task-id}           - changes a task's status to "in-progress"
  mark-done {task-id}                  - changes a task's status to "done"
  list                                 - displays all tasks
  list to-do                           - displays tasks with "to-do" status
  list in-progress                     - displays tasks with "in-progress" status
  list done                            - displays tasks with "done" status
```
Use the command that you need, e.g., `add`:
```bash
dotnet run -- add "Study math"
```
Output:
```

(]=====================================================================================[)
||  Id  ||  Description  ||  Status  ||       CreatedAt       ||       UpdatedAt       ||
||=====================================================================================||
||  0   ||  Study math   ||   ToDo   ||  11/09/2025 09:46:33  ||  11/09/2025 09:46:33  ||
(]=====================================================================================[)

```
> [!NOTE]
> `add`, `update`, `mark-in-progress`, and `mark-done` commands also display the row affected by the operation.
## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
