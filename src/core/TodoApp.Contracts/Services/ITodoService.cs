using System.Dynamic;
using TodoApp.Entities;
using TodoApp.Models.Dtos;
using TodoApp.Models.Paging;
using TodoApp.Models.Parameters;
using Tasks = System.Threading.Tasks;

namespace TodoApp.Contracts.Services
{
    public interface ITodoService
    {
        Task<(IEnumerable<ExpandoObject> Dto, PageInfo PageInfo)> GetTodosAsync(TodoParameters parameters);
        Task<ExpandoObject> GetTodoAsync(int id, TodoParameters parameters);
        Task<TodoDto> CreateTodoAsync(TodoForCreationDto requestDto);
        Tasks.Task DeleteTodoAsync(int id);
        Tasks.Task UpdateTodoAsync(int id, TodoForUpdateDto requestDto);
        Task<(TodoForUpdateDto DtoToPatch, Todo Entity)> GetTodoForPatchAsync(int id);
        Tasks.Task UpdateTodoFromPatchAsync(TodoForUpdateDto requestDto, Todo entity);
    }
}
