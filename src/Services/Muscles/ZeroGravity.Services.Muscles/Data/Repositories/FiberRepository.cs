using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Muscles.Data.Entities;
using ZeroGravity.Services.Muscles.Data.Persistence;

namespace ZeroGravity.Services.Muscles.Data.Repositories;

public interface IFiberRepository : IRepository<Fiber, int>
{
    public Task<Fiber?> GetByNameAsync(string name, bool track = true);
}

public class FiberRepository : RepositoryBase<Fiber, int, MuscleDbContext>, IFiberRepository
{
    public FiberRepository(MuscleDbContext context) : base(context)
    {
        
    }

    public async Task<Fiber?> GetByNameAsync(string name, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Fiber>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Name == name);
        }
        
        return await Context
            .Set<Fiber>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == name);
    }
}