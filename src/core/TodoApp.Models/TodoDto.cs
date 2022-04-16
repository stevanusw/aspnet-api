namespace TodoApp.Models
{
    public record TodoDto(int Id,
        string Name,
        bool isCompleted,
        DateTime CreateDate);
}