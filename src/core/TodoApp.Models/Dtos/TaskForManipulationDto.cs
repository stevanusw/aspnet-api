using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Dtos
{
    public abstract record TaskForManipulationDto
    {
        [Required]
        public string? Name { get; init; }
    }
}
