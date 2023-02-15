using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

public interface IWorkoutRepository : IRepository<Entities.Workout, int>
{
    
}

public class WorkoutRepository : RepositoryBase<Entities.Workout, int, WorkoutDbContext>, IWorkoutRepository
{
    public WorkoutRepository(WorkoutDbContext context) : base(context)
    {
    }
}
