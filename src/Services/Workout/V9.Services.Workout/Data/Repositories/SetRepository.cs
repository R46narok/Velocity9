using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Persistence;

namespace V9.Services.Workout.Data.Repositories;

public interface ISetRepository : IRepository<Set, int>
{
    public Task<Set?> GetByIndexAsync(string userName, string workoutName, int index, bool track = true);
}

public class SetRepository : RepositoryBase<Set, int, WorkoutDbContext>, ISetRepository
{
    public SetRepository(WorkoutDbContext context) : base(context)
    {
    }

    public async Task<Set?> GetByIndexAsync(string userName, string workoutName, int index, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Set>()
                .AsTracking()
                .Where(x => x.Workout.Name == workoutName && x.Workout.User.UserName == userName)
                .Skip(index)
                .Take(1)
                .SingleOrDefaultAsync();
        }
        
        return await Context
            .Set<Set>()
            .AsNoTracking()
            .Where(x => x.Workout.Name == workoutName && x.Workout.User.UserName == userName)
            .Skip(index)
            .Take(1)
            .SingleOrDefaultAsync();
    }
}