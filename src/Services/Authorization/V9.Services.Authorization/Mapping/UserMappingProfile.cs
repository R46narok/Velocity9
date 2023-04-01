using AutoMapper;
using V9.Services.Authorization.Commands.Users.CreateUser;
using V9.Services.Authorization.Commands.Users.DeleteUser;
using V9.Services.Authorization.Data.Entities;
using V9.Services.Authorization.Dto;
using V9.Services.Authorization.Commands.Users;

namespace V9.Services.Authorization.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserCredentialsDto, User>();
        CreateMap<UserCredentialsDto, CreateUserCommand>();

        CreateMap<User, UserDto>();
        // CreateMap<User, UserView>();
        CreateMap<CreateUserCommand, User>();

        CreateMap<User, UserCreatedEvent>();
        CreateMap<User, UserDeletedEvent>();
        // CreateMap<UpdateUserDto, UpdateUserCommand>();
        // CreateMap<UpdateUserCommand, User>();
    }
}