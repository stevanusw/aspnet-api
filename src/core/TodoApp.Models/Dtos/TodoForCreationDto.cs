namespace TodoApp.Models.Dtos
{
    public record TodoForCreationDto(string Name, IEnumerable<TaskForCreationDto>? Tasks);
}
