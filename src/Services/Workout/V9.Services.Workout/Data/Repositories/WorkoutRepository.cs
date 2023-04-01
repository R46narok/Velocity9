using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Workout.Data.Persistence;

namespace V9.Services.Workout.Data.Repositories;

public interface IWorkoutRepository : IRepository<Entities.Workout, int>
{
    public Task<Entities.Workout?> GetByNameAsync(string userName, string workoutName, bool track = true);
    public Task<Entities.Workout?> GetLastAsync(string userName, bool track = true);
    public Task<List<Entities.Workout>> GetAll(string userName, bool track = true);
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
                .ThenInclude(x => x.Exercise)
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Name == workoutName && x.User.UserName == userName);
        }

        return await Context
            .Set<Entities.Workout>()
            .Include(x => x.User)
            .Include(x => x.Sets)
            .ThenInclude(x => x.Exercise)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == workoutName && x.User.UserName == userName);
    }

    public async Task<Entities.Workout?> GetLastAsync(string userName, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Entities.Workout>()
                .Include(x => x.Sets)
                .ThenInclude(x => x.Exercise)
                .OrderBy(x => x.CompletedOn)
                .AsTracking()
                .FirstOrDefaultAsync(x => x.User.UserName == userName);
        }

        return await Context
            .Set<Entities.Workout>()
            .Include(x => x.Sets)
            .ThenInclude(x => x.Exercise)
            .OrderBy(x => x.CompletedOn)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.User.UserName == userName);
    }

    public async Task<List<Entities.Workout>> GetAll(string userName, bool track = true)
    {
        return await Context
            .Set<Entities.Workout>()
            .AsNoTracking()
            .Where(x => x.User.UserName == userName)
            .ToListAsync();
    }
}
