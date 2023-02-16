using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

public interface IExerciseRepository : IRepository<Exercise, int>
{
    public Task<Exercise?> GetByNameAsync(string name, bool track = true);

}

public class ExerciseRepository : RepositoryBase<Exercise, int, WorkoutDbContext>, IExerciseRepository
{
    public ExerciseRepository(WorkoutDbContext context) : base(context)
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

    public override List<Exercise> GetAll()
    {
        return Context
            .Exercises
            .AsNoTracking()
            .ToList();
    }
}
