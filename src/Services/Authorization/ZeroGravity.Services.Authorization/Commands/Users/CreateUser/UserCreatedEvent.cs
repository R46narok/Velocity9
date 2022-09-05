using ZeroGravity.Domain.Events;
using ZeroGravity.Services.Authorization.Dto;

namespace ZeroGravity.Services.Authorization.Commands.Users.CreateUser;

public class UserCreatedEvent : IDomainEvent
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}