namespace TodoApp.Entities
{
    public class Todo : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
