﻿using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

public interface IUserRepository : IRepository<User, int>
{
    public Task<User?> GetByNameAsync(string name, bool track = true);
    public Task<User?> GetByExternalIdAsync(string id, bool track = true);
}

public class UserRepository : RepositoryBase<User, int, WorkoutDbContext>, IUserRepository
{
    public UserRepository(WorkoutDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByNameAsync(string name, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<User>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.UserName == name);
        }
        
        return await Context
            .Set<User>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserName == name);
    }

    public async Task<User?> GetByExternalIdAsync(string id, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<User>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.ExternalId == id);
        }

        return await Context
            .Set<User>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.ExternalId == id);
    }
}