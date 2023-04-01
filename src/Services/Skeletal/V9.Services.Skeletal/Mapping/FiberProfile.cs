using AutoMapper;
using V9.Services.Skeletal.Commands;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Dto;

namespace V9.Services.Skeletal.Mapping;

public class FiberProfile : Profile
{
    public FiberProfile()
    {
        CreateMap<CreateFiberCommand, Fiber>();
        CreateMap<Fiber, FiberDto>();
    }
}