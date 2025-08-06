using AutoMapper;
using TodoAppSnowlyCode.Data.Models;
using TodoAppSnowlyCode.DTO;

namespace TodoAppSnowlyCode.AutoMapping
{
    internal class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<CreateToDoItemDto, ToDoItem>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateToDoItemDto, ToDoItem>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<ToDoItem, GetToDoItemDto>();
        }
    }
}
