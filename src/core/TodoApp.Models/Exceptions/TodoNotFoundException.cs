namespace TodoApp.Models.Exceptions
{
    public class TodoNotFoundException : NotFoundException
    {
        public TodoNotFoundException(int id) : base($"Todo with id: {id} does not exist.")
        {
        }
    }
}
