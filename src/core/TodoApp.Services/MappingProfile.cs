using AutoMapper;
using TodoApp.Models;
using Entities = TodoApp.Domain.Entities;

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
