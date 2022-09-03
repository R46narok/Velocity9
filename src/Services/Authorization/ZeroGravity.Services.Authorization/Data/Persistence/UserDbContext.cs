using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZeroGravity.Services.Authorization.Data.Entities;

namespace ZeroGravity.Services.Authorization.Data.Persistence;

public class UserDbContext : IdentityDbContext<User>
{
    public UserDbContext()
    {
        
    }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
}