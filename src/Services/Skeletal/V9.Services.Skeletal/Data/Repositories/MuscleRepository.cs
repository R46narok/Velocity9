using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Persistence;

namespace V9.Services.Skeletal.Data.Repositories;

public interface IMuscleRepository : IRepository<Muscle, int>
{
    public Task<Muscle?> GetByNameAsync(string name, bool track = true);
}

public class MuscleRepository : RepositoryBase<Muscle, int, SkeletalDbContext>, IMuscleRepository
{
    public MuscleRepository(SkeletalDbContext context) : base(context)
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