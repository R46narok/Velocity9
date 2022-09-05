using Microsoft.EntityFrameworkCore;
using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Data.Persistence;

namespace ZeroGravity.Services.Exercises.Data.Repositories;

public interface IAuthorRepository : IRepository<Author, int>
{
    public Task<Author?> GetByExternalIdAsync(string externalId, bool track = true);
    public Task<Author?> GetByUserNameAsync(string userName, bool track = true);
}

public class AuthorRepository : RepositoryBase<Author, int, ExercisesDbContext>, IAuthorRepository
{
    public AuthorRepository(ExercisesDbContext context) : base(context)
    {
    }

    public async Task<Author?> GetByExternalIdAsync(string externalId, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Author>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.ExternalId == externalId);
        }

        return await Context
            .Set<Author>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.ExternalId == externalId);
    }

    public async Task<Author?> GetByUserNameAsync(string userName, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<Author>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.UserName == userName);
        }

        return await Context
            .Set<Author>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserName == userName);
    }
}