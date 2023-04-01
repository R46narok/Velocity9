using V9.Domain.Events;
using V9.Services.Authorization.Dto;

namespace V9.Services.Authorization.Commands.Users.CreateUser;

public class UserCreatedEvent : IDomainEvent
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}