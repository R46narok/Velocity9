using AutoMapper;
using ZeroGravity.Services.Coach.Commands;
using ZeroGravity.Services.Coach.Data.Entities;

namespace ZeroGravity.Services.Coach.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<CreateExerciseCommand, Exercise>()
            .ForMember(x => x.Name, opt => opt.MapFrom(t => t.Name))
            .ForMember(x => x.ExternalId, opt => opt.MapFrom(t => t.ExternalId))
            .ReverseMap();
    }
}