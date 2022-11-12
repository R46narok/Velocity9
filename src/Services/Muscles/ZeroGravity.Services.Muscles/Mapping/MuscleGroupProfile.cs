using AutoMapper;
using ZeroGravity.Services.Muscles.Commands;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Dto;

namespace ZeroGravity.Services.Muscles.Mapping;

public class MuscleGroupProfile : Profile
{
    public MuscleGroupProfile()
    {
        CreateMap<CreateMuscleGroupCommand, MuscleGroup>();
        CreateMap<MuscleGroup, MuscleGroupDto>();
    }
}