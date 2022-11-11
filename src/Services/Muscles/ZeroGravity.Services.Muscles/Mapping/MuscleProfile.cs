using AutoMapper;
using ZeroGravity.Services.Muscles.Commands;
using ZeroGravity.Services.Muscles.Data.Entities;

namespace ZeroGravity.Services.Muscles.Mapping;

public class MuscleProfile : Profile
{
    public MuscleProfile()
    {
        CreateMap<CreateMuscleCommand, Muscle>()
            .ForMember(x => x.Group, opt => opt.Ignore());
    }
}