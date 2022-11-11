using AutoMapper;
using ZeroGravity.Services.Muscles.Commands;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Dto;

namespace ZeroGravity.Services.Muscles.Mapping;

public class FiberProfile : Profile
{
    public FiberProfile()
    {
        CreateMap<CreateFiberCommand, Fiber>();
        CreateMap<Fiber, FiberDto>();
    }
}