using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Data.Persistence;

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