using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Remotes;

namespace V9.Services.Workout.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RemoteUser, CreateUserCommand>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();

        CreateMap<CreateUserCommand, User>();

    }
}