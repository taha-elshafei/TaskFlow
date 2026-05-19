namespace TaskFlow.Domain.Constants;

public static class PermissionConstants
{
    public const string ProjectsCreate = "Projects.Create";
    public const string ProjectsRead = "Projects.Read";
    public const string ProjectsUpdate = "Projects.Update";
    public const string ProjectsDelete = "Projects.Delete";

    public const string TasksCreate = "Tasks.Create";
    public const string TasksRead = "Tasks.Read";
    public const string TasksUpdate = "Tasks.Update";
    public const string TasksDelete = "Tasks.Delete";

    public static readonly string[] All =
    [
        ProjectsCreate, ProjectsRead, ProjectsUpdate, ProjectsDelete,
        TasksCreate, TasksRead, TasksUpdate, TasksDelete
    ];
}
