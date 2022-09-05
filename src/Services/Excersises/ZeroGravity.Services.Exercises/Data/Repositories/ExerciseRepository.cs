using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Persistence;

namespace ZeroGravity.Services.Exercises.Data.Repositories;

public interface IExerciseRepository : IRepository<Exercise, int>
{
    public Task<Exercise?> GetByNameAsync(string name, bool track = true);
}

public class ExerciseRepository : RepositoryBase<Exercise, int, ExercisesDbContext>, IExerciseRepository
{
    public ExerciseRepository(ExercisesDbContext context) : base(context)
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

    public override async Task<Exercise?> GetByIdAsync(int id, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Exercise>()
                .Include(x => x.Author)
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        
        return await Context
            .Set<Exercise>()
            .Include(x => x.Author)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
   }
}