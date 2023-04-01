using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using V9.Services.Authorization.Data.Entities;

namespace V9.Services.Authorization.Data.Persistence;

public class UserDbContext : IdentityDbContext<User>
{
    public UserDbContext()
    {
        
    }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
}