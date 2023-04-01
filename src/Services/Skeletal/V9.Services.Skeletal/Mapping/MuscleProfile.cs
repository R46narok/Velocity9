using AutoMapper;
using V9.Services.Skeletal.Commands;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Dto;

namespace V9.Services.Skeletal.Mapping;

public class MuscleProfile : Profile
{
    public MuscleProfile()
    {
        CreateMap<CreateMuscleCommand, Muscle>()
            .ForMember(x => x.Group, opt => opt.Ignore());
        CreateMap<Muscle, MuscleDto>();
    }
}