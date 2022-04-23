using AutoMapper;
using TodoApp.Models.Dtos;

namespace TodoApp.Services
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Todo, TodoDto>();
            CreateMap<Entities.Task, TaskDto>();

            CreateMap<TodoForCreationDto, Entities.Todo>();
        }
    }
}
