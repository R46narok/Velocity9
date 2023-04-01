using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Persistence;

namespace V9.Services.Workout.Data.Repositories;


public interface IMuscleRepository : IRepository<Muscle, int>
{
    public Task<Muscle?> GetByNameAsync(string name, bool track = true);
}

public class MuscleRepository : RepositoryBase<Muscle, int, WorkoutDbContext>, IMuscleRepository
{
    public MuscleRepository(WorkoutDbContext context) : base(context)
    {
    }

    public async Task<Muscle?> GetByNameAsync(string name, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Muscle>()
                .AsTracking()
                // .Include(m => m.Group)
                .SingleOrDefaultAsync(x => x.Name == name);
        }
        
        return await Context
            .Set<Muscle>()
            .AsNoTracking()
            // .Include(m => m.Group)
            .SingleOrDefaultAsync(x => x.Name == name);
    }
}
