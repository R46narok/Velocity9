using Microsoft.AspNetCore.Identity;
using V9.Domain.Entities;

namespace V9.Services.Authorization.Data.Entities;

public sealed class User : IdentityUser, IEntity<string>
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public User()
    {
        
    }
    
    public User(string username)
    {
        UserName = username;
    }
}