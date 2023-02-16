using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;


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
