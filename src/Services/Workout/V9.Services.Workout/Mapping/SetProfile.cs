using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Dto;

namespace V9.Services.Workout.Mapping;

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