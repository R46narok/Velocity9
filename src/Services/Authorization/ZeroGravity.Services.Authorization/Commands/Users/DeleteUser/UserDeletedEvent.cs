using ZeroGravity.Domain.Events;

namespace ZeroGravity.Services.Authorization.Commands.Users.DeleteUser;

public class UserDeletedEvent : IDomainEvent
{
    public string Id { get; set; }
    public string UserName { get; set; } 
    
    public UserDeletedEvent(string userName, string id)
    {
        UserName = userName;
        Id = id;
    }
}