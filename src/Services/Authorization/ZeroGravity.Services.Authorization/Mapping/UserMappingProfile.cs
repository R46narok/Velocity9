using AutoMapper;
using ZeroGravity.Services.Authorization.Commands.Users;
using ZeroGravity.Services.Authorization.Commands.Users.CreateUser;
using ZeroGravity.Services.Authorization.Commands.Users.DeleteUser;
using ZeroGravity.Services.Authorization.Data.Entities;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Services.Authorization.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserCredentialsDto, User>();
        CreateMap<UserCredentialsDto, CreateUserCommand>();

        CreateMap<User, UserDto>();
        // CreateMap<User, UserView>();
        CreateMap<CreateUserCommand, User>();

        CreateMap<User, UserCreatedEvent>()
            .ForMember(x => x.Data,
                d => d.MapFrom(x => x));

        CreateMap<User, UserDeletedEvent>();
        // CreateMap<UpdateUserDto, UpdateUserCommand>();
        // CreateMap<UpdateUserCommand, User>();
    }
}