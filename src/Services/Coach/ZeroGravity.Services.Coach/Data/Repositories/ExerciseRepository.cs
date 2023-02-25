using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Coach.Data.Entities;
using ZeroGravity.Services.Coach.Data.Persistence;

namespace ZeroGravity.Services.Coach.Data.Repositories;

public interface IExerciseRepository : IRepository<Exercise, int>
{
    public Task<Exercise?> GetExerciseByName(string name, bool track = true);
    public Task<Exercise?> GetExerciseByExternalId(int externalId, bool track = true);
}

public class ExerciseRepository : RepositoryBase<Exercise, int, CoachDbContext>, IExerciseRepository 
{
    public ExerciseRepository(CoachDbContext context) : base(context)
    {
    }

    public async Task<Exercise?> GetExerciseByName(string name, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Exercise>()
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        return await Context
            .Set<Exercise>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Exercise?> GetExerciseByExternalId(int externalId, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Exercise>()
                .AsTracking()
                .FirstOrDefaultAsync(x => x.ExternalId == externalId);
        }

        return await Context
            .Set<Exercise>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ExternalId == externalId);
    }
}