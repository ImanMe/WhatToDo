using AutoMapper;
using WhatToDo.Application.Dtos;
using WhatToDo.Core.Entities;

namespace WhatToDo.Application.MappingProfiles;

public class ToDoItemMappingProfile : Profile
{
    public ToDoItemMappingProfile()
    {
        CreateMap<CreateItemDto, ToDoItem>();
        CreateMap<UpdateItemDto, ToDoItem>();
        CreateMap<ToDoItem, ItemResponseDto>()
            .ForMember(x => x.CreatedDate, o => o.MapFrom(src => src.CreatedDate.ToShortDateString()));
    }
}