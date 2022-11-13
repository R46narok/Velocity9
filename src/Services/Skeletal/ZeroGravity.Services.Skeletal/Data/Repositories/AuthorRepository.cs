using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Data.Persistence;

namespace ZeroGravity.Services.Skeletal.Data.Repositories;

public interface IAuthorRepository : IRepository<Author, int>
{
    
}

public class AuthorRepository : RepositoryBase<Author, int, SkeletalDbContext>, IAuthorRepository
{
    public AuthorRepository(SkeletalDbContext context) : base(context)
    {
    }
}