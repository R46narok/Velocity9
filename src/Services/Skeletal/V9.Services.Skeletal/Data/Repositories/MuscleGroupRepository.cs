using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Persistence;

namespace V9.Services.Skeletal.Data.Repositories;

public interface IMuscleGroupRepository : IRepository<MuscleGroup, int>
{
    public Task<MuscleGroup?> GetByNameAsync(string name, bool track = true);
}

public class MuscleGroupRepository : RepositoryBase<MuscleGroup, int, SkeletalDbContext>, IMuscleGroupRepository
{
    public MuscleGroupRepository(SkeletalDbContext context) : base(context)
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