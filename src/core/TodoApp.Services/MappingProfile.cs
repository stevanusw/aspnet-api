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
            CreateMap<TaskForCreationDto, Entities.Task>();
            CreateMap<TodoForUpdateDto, Entities.Todo>()
                .ReverseMap();
            CreateMap<TaskForUpdateDto, Entities.Task>();
        }
    }
}
