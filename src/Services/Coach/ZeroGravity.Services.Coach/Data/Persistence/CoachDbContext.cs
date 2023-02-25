using Microsoft.EntityFrameworkCore;

namespace ZeroGravity.Services.Coach.Data.Persistence;

public class CoachDbContext : DbContext
{
    public CoachDbContext()
    {
        
    }

    public CoachDbContext(DbContextOptions<CoachDbContext> options) : base(options)
    {
        
    }
}