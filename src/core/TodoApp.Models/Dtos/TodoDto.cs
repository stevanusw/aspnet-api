namespace TodoApp.Models.Dtos
{
    public record TodoDto(int Id, string Name, bool isCompleted, DateTime CreateDateUtc);
}