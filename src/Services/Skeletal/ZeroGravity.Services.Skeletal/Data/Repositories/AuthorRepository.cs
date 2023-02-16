using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Persistence;

namespace ZeroGravity.Services.Skeletal.Data.Repositories;

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