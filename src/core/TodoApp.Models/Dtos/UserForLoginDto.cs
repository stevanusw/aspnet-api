using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Dtos
{
    public record UserForLoginDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}
