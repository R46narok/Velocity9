using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Data.Persistence;

namespace ZeroGravity.Services.Muscles.Data.Repositories;

public interface IMuscleRepository : IRepository<Muscle, int>
{
    public Task<Muscle?> GetByNameAsync(string name, bool track = true);
}

public class MuscleRepository : RepositoryBase<Muscle, int, MuscleDbContext>, IMuscleRepository
{
    public MuscleRepository(MuscleDbContext context) : base(context)
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