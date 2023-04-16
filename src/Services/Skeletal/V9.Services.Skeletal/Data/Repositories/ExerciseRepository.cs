﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Persistence;

namespace V9.Services.Skeletal.Data.Repositories;

public interface IExerciseRepository : IRepository<Exercise, int>
{
    public Task<Exercise?> GetByNameAsync(string name, bool track = true);

}

public class ExerciseRepository : RepositoryBase<Exercise, int, SkeletalDbContext>, IExerciseRepository
{
    public ExerciseRepository(SkeletalDbContext context) : base(context)
    {
    }

    public async Task<Exercise?> GetByNameAsync(string name, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Exercise>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Name == name);
        }
        
        return await Context
            .Set<Exercise>()
            .AsNoTracking()
            .Include(x => x.Targets)
            .Include(x => x.Author)
            .SingleOrDefaultAsync(x => x.Name == name);
    }

    public override List<Exercise> GetAll()
    {
        return Context
            .Exercises
            .Include(x => x.Targets)
            .Include(x => x.Author)
            .AsNoTracking()
            .ToList();
    }
}