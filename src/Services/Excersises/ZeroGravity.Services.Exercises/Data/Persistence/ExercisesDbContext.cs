using Microsoft.EntityFrameworkCore;
using ZeroGravity.Services.Exercises.Data.Entities;

namespace ZeroGravity.Services.Exercises.Data.Persistence;

public class ExercisesDbContext : DbContext
{
    public DbSet<Exercise>? Exercises { get; set; }
    public DbSet<Muscle>? TargetMuscles { get; set; }
    public DbSet<Author?> Authors { get; set; } = null!;

    public ExercisesDbContext()
    {
        
    }

    public ExercisesDbContext(DbContextOptions<ExercisesDbContext> options) : base(options)
    {
        
    }
}