using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Dtos
{
    public record TokenForRefreshDto()
    {
        [Required]
        public string? RefreshToken { get; init; }
    }
}
