﻿using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

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