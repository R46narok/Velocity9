using Microsoft.EntityFrameworkCore;
using V9.Application.Interfaces;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Data.Persistence;

namespace V9.Services.Skeletal.Data.Repositories;

public interface IAuthorRepository : IRepository<Author, int>
{
    public Task<Author?> GetByNameAsync(string name, bool track = true);
    public Task<Author?> GetByExternalIdAsync(string id, bool track = true);
}

public class AuthorRepository : RepositoryBase<Author, int, SkeletalDbContext>, IAuthorRepository
{
    public AuthorRepository(SkeletalDbContext context) : base(context)
    {
    }

    public async Task<Author?> GetByNameAsync(string name, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Author>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.UserName == name);
        }
        
        return await Context
            .Set<Author>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserName == name);
    }

    public async Task<Author?> GetByExternalIdAsync(string id, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Author>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.ExternalId == id);
        }

        return await Context
            .Set<Author>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.ExternalId == id);
    }
}