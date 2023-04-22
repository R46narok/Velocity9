using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Enums;

namespace V9.Services.Skeletal.Data.Persistence;

public sealed class SkeletalDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<Fiber> Fibers { get; set; } = null!;
    public DbSet<Muscle> Muscles { get; set; } = null!;
    public DbSet<MuscleGroup> MuscleGroups { get; set; } = null!;

    public SkeletalDbContext()
    {
        
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public SkeletalDbContext(DbContextOptions<SkeletalDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Fiber>()
            .Property(e => e.TwitchForce)
            .HasConversion(
            v => v.ToString(),
            v => (TwitchForce)Enum.Parse(typeof(TwitchForce), v));
  
        modelBuilder
            .Entity<Fiber>()
            .Property(e => e.TwitchSpeed)
            .HasConversion(
            v => v.ToString(),
            v => (TwitchSpeed)Enum.Parse(typeof(TwitchSpeed), v));
  
        
        modelBuilder
            .Entity<Exercise>()
            .Property(e => e.Difficulty)
            .HasConversion(
            v => v.ToString(),
            v => (ExerciseDifficulty)Enum.Parse(typeof(ExerciseDifficulty), v));
        
        modelBuilder
            .Entity<Exercise>()
            .Property(e => e.ExecutionSteps)
            .HasConversion(
                            v => string.Join(';', v),
                            v => v.Split(';', StringSplitOptions.RemoveEmptyEntries))
            .Metadata.SetValueComparer(new ValueComparer<string[]>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (string[])c.ToArray().Clone()));
        
        modelBuilder
            .Entity<Fiber>()
            .Property(e => e.MotorUnitType)
            .HasConversion(
            v => v.ToString(),
            v => (MotorUnitType)Enum.Parse(typeof(MotorUnitType), v));
  
        modelBuilder
            .Entity<Fiber>()
            .Property(e => e.ResistanceToFatigue)
            .HasConversion(
            v => v.ToString(),
            v => (ResistanceToFatigue)Enum.Parse(typeof(ResistanceToFatigue), v));
    }

}