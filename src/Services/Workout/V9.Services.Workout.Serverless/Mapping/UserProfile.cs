using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Serverless.Users.Queue;

namespace V9.Services.Workout.Serverless.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreatedEvent, CreateUserCommand>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(t => t.UserName))
            .ForMember(x => x.ExternalId, opt => opt.MapFrom(t => t.Id))
            .ReverseMap();
    }
}