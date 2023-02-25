using AutoMapper;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Dto;

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

        CreateMap<Set, SetDto>()
            .ForMember(x => x.ExerciseName, opt => opt.MapFrom(t => t.Exercise.Name))
            .ReverseMap();
    }
}