using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Services.Authorization.Commands.Users.CreateUser;

public class UserCreatedEvent : IDomainEvent
{
    public UserDto Data { get; set; }
}