namespace TodoApp.Models.Dtos
{
    public record TodoForUpdateDto(string Name, IEnumerable<TaskForCreationDto>? Tasks);
}
