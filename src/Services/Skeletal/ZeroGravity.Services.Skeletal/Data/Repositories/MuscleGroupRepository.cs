using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Persistence;

namespace ZeroGravity.Services.Skeletal.Data.Repositories;

public interface IMuscleGroupRepository : IRepository<MuscleGroup, int>
{
    public Task<MuscleGroup?> GetByNameAsync(string name, bool track = true);
}

public class MuscleGroupRepository : RepositoryBase<MuscleGroup, int, MuscleDbContext>, IMuscleGroupRepository
{
    public MuscleGroupRepository(MuscleDbContext context) : base(context)
    {
    }

    public async Task<MuscleGroup?> GetByNameAsync(string name, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<MuscleGroup>()
                .AsTracking()
                .Include(m => m.Muscles)
                .SingleOrDefaultAsync(x => x.Name == name);
        }
        
        return await Context
            .Set<MuscleGroup>()
            .AsNoTracking()
            .Include(m => m.Muscles)
            .SingleOrDefaultAsync(x => x.Name == name);
    }

    public override List<MuscleGroup> GetAll()
    {
        return Context
            .Set<MuscleGroup>()
            .AsQueryable()
            .Include(x => x.Muscles)
            .ToList();
    }
}