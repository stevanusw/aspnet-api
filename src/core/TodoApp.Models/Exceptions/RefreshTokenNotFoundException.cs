namespace TodoApp.Models.Exceptions
{
    public class RefreshTokenNotFoundException : NotFoundException
    {
        public RefreshTokenNotFoundException() : base($"Refresh token does not exist.")
        {
        }
    }
}
