using Microsoft.EntityFrameworkCore;
using ZeroGravity.Services.Workout.Data.Entities;

namespace ZeroGravity.Services.Workout.Data.Persistence;

#nullable disable

public class WorkoutDbContext : DbContext
{
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Muscle> Muscles { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<Entities.Workout> Workouts { get; set; }

    public WorkoutDbContext()
    {
        
    }

    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Entities.Workout>()
            .Property(e => e.Type)
            .HasConversion(
            v => v.ToString(),
            v => (WorkoutType)Enum.Parse(typeof(WorkoutType), v));
    }
}