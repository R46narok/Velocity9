using AutoMapper;
using ZeroGravity.Services.Skeletal.Commands;
using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Mapping;

public class MuscleProfile : Profile
{
    public MuscleProfile()
    {
        CreateMap<CreateMuscleCommand, Muscle>()
            .ForMember(x => x.Group, opt => opt.Ignore());
    }
}