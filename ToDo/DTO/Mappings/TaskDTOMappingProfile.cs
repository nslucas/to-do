using AutoMapper;
using ToDo.Models;
using TaskModel = ToDo.Models.Task;

namespace ToDo.DTO.Mappings
{
    public class TaskDTOMappingProfile : Profile
    {
        public TaskDTOMappingProfile()
        {
            CreateMap<TaskModel, TaskDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
        }
    }
}
