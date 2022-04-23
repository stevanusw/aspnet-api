namespace TodoApp.Models.Exceptions
{
    public class TaskNotFoundException : NotFoundException
    {
        public TaskNotFoundException(int id) : base($"Task with id: {id} does not exist.")
        {
        }
    }
}
