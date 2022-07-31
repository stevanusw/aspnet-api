namespace TodoApp.Models.Exceptions
{
    public class RefreshTokenBadRequestException : BadRequestException
    {
        public RefreshTokenBadRequestException() : base("Invalid request token.")
        {
        }
    }
}
