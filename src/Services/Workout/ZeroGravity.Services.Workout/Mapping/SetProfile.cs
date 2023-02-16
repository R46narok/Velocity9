using AutoMapper;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Entities;

namespace ZeroGravity.Services.Workout.Mapping;

public class SetProfile : Profile
{
    public SetProfile()
    {
        CreateMap<CreateSetCommand, Set>()
            .ReverseMap();
        CreateMap<Set, SetCreatedEvent>()
            .ReverseMap();
        CreateMap<Set, SetDeletedEvent>()
            .ReverseMap();
    }
}