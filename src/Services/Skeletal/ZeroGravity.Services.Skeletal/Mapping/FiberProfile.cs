using AutoMapper;
using ZeroGravity.Services.Skeletal.Commands;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Mapping;

public class FiberProfile : Profile
{
    public FiberProfile()
    {
        CreateMap<CreateFiberCommand, Fiber>();
        CreateMap<Fiber, FiberDto>();
    }
}