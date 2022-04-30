namespace TodoApp.Models.Dtos
{
    public record TodoForUpdateDto(bool IsCompleted) : TodoForManipulationDto;
}
