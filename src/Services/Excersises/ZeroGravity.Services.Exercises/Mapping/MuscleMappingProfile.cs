using AutoMapper;
using ZeroGravity.Services.Exercises.Commands.Muscles;
using ZeroGravity.Services.Exercises.Commands.Muscles.CreateMuscle;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Dto;
using ZeroGravity.Services.Exercises.Queries.Muscles.GetMuscle;

namespace ZeroGravity.Services.Exercises.Mapping;

public class MuscleMappingProfile : Profile
{
    public MuscleMappingProfile()
    {
        CreateMap<CreateMuscleCommand, Muscle>();
        CreateMap<Muscle, MuscleCreatedEvent>();

        CreateMap<GetMuscleQuery, Muscle>();
        CreateMap<Muscle, MuscleDto>();
    }
}