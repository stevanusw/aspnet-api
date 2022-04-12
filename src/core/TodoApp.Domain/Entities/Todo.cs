namespace TodoApp.Domain.Entities
{
    public class Todo : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
