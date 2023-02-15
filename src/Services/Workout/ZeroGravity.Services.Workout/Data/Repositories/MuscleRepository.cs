using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

public interface IMuscleRepository : IRepository<Muscle, int>
{
    
}

public class MuscleRepository : RepositoryBase<Muscle, int, WorkoutDbContext>, IMuscleRepository
{
    public MuscleRepository(WorkoutDbContext context) : base(context)
    {
    }
}
