namespace TodoApp.Models.Dtos
{
    public record TodoForCreationDto(string name, IEnumerable<TaskForCreationDto> tasks);
}
