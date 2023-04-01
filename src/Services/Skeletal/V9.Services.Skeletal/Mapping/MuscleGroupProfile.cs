using AutoMapper;
using V9.Services.Skeletal.Commands;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Dto;

namespace V9.Services.Skeletal.Mapping;

public class MuscleGroupProfile : Profile
{
    public MuscleGroupProfile()
    {
        CreateMap<CreateMuscleGroupCommand, MuscleGroup>();
        CreateMap<MuscleGroup, MuscleGroupDto>();
    }
}