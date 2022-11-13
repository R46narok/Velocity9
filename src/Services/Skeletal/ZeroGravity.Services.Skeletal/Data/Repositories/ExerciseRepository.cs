using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Persistence;

namespace ZeroGravity.Services.Skeletal.Data.Repositories;

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
            .SingleOrDefaultAsync(x => x.Name == name);
    }
}