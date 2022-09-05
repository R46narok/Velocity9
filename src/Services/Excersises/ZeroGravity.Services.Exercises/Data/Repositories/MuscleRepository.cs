using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Persistence;

namespace ZeroGravity.Services.Exercises.Data.Repositories;

public interface IMuscleRepository : IRepository<Muscle, int>
{
    public Task<Muscle?> GetByGroupAsync(string group, bool track = true);
}

public class MuscleRepository : RepositoryBase<Muscle, int, ExercisesDbContext>, IMuscleRepository
{
    public MuscleRepository(ExercisesDbContext context) : base(context)
    {
    }

    public async Task<Muscle?> GetByGroupAsync(string group, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Muscle>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Group == group);
        }
        return await Context
            .Set<Muscle>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Group == group);
    }
}