using AutoMapper;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Remotes;

namespace ZeroGravity.Services.Workout.Mapping;

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