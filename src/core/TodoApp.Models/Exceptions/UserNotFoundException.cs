namespace TodoApp.Models.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base($"User does not exist.")
        {
        }
    }
}
