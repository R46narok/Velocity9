using AutoMapper;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Remotes;
using ZeroGravity.Services.Workout.Handlers;

namespace ZeroGravity.Services.Workout.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreatedEvent, CreateUserCommand>()
            .ForMember(x => x.ExternalId, f => f.MapFrom(t => t.Id));

        CreateMap<RemoteUser, CreateUserCommand>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ReverseMap();

        CreateMap<CreateUserCommand, User>();

    }
}