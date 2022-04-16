namespace TodoApp.Domain.Entities
{
    public class Task : BaseEntity
    {
        public int TodoId { get; set; }
        public string? Name { get; set; }      
        public Todo? TodoNavigation { get; set; }
    }
}
