using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Remotes;

namespace V9.Services.Workout.Mapping;

public class MuscleProfile : Profile
{
    public MuscleProfile()
    {
        CreateMap<RemoteMuscle, CreateMuscleCommand>()
            .ForMember(x => x.Name, opt => opt.MapFrom(t => t.Name))
            .ForMember(x => x.ExternalId, opt => opt.MapFrom(t => t.Id))
            .ReverseMap();

        CreateMap<CreateMuscleCommand, Muscle>()
            .ReverseMap();
    }
}