namespace todolist.EfCore
{
    public class AddTaskTDb
    {
        public string Title { get; set; } = string.Empty;
        public string? assignmentDescription { get; set; }
        public bool isDone { get; set; }
    }
}
