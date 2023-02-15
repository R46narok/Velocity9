using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

public interface ISetRepository : IRepository<Set, int>
{
    
}

public class SetRepository : RepositoryBase<Set, int, WorkoutDbContext>, ISetRepository
{
    public SetRepository(WorkoutDbContext context) : base(context)
    {
    }
}