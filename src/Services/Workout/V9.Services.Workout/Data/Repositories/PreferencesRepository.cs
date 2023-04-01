using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Persistence;

namespace V9.Services.Workout.Data.Repositories;

public interface IPreferencesRepository : IRepository<Preferences, int>
{
    public Task<Preferences?> GetByUserNameAsync(string userName, bool track = true);
}

public class PreferencesRepository : RepositoryBase<Preferences, int, WorkoutDbContext>, IPreferencesRepository
{
    public PreferencesRepository(WorkoutDbContext context) : base(context)
    {
    }

    public async Task<Preferences?> GetByUserNameAsync(string userName, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Preferences>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.User.UserName == userName);
        }

        return await Context
            .Set<Preferences>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.User.UserName == userName);
    }
}