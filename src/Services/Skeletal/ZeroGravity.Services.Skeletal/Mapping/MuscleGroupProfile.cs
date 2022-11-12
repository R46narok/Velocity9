using AutoMapper;
using ZeroGravity.Services.Skeletal.Commands;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Mapping;

public class MuscleGroupProfile : Profile
{
    public MuscleGroupProfile()
    {
        CreateMap<CreateMuscleGroupCommand, MuscleGroup>();
        CreateMap<MuscleGroup, MuscleGroupDto>();
    }
}