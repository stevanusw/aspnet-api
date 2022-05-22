using System.Dynamic;

namespace TodoApp.Models.Dtos
{
    public record ShapedDto(int Id, ExpandoObject Entity);
}
