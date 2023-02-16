using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

public interface IWorkoutRepository : IRepository<Entities.Workout, int>
{
    public Task<Entities.Workout?> GetByNameAsync(string userName, string workoutName, bool track = true);
}

public class WorkoutRepository : RepositoryBase<Entities.Workout, int, WorkoutDbContext>, IWorkoutRepository
{
    public WorkoutRepository(WorkoutDbContext context) : base(context)
    {
    }

    public async Task<Entities.Workout?> GetByNameAsync(string userName, string workoutName, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Entities.Workout>()
                .Include(x => x.User)
                .Include(x => x.Sets)
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Name == workoutName && x.User.UserName == userName);
        }

        return await Context
            .Set<Entities.Workout>()
            .Include(x => x.User)
            .Include(x => x.Sets)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == workoutName && x.User.UserName == userName);
    }
}
