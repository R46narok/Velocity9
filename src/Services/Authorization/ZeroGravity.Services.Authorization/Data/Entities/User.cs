using Microsoft.AspNetCore.Identity;
using ZeroGravity.Domain.Entities;

namespace ZeroGravity.Services.Authorization.Data.Entities;

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