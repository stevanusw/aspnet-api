using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models.Dtos
{
    public abstract record TodoForManipulationDto
    {
        [Required]
        public string? Name { get; init; }
        public IEnumerable<TaskForCreationDto>? Tasks { get; init; }
    }
}
