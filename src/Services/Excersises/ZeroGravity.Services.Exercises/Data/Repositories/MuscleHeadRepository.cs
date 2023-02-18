using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Exercises.Data.Entities;

namespace ZeroGravity.Services.Exercises.Data.Repositories;

public interface IMuscleHeadRepository : IRepository<MuscleHead, int>
{
    public Task<>
}

public class MuscleHeadRepository : RepositoryBase<MuscleHead, int, ExerciseDbContext>, IMuscleHeadRepository
{
    public MuscleHeadRepository(ExerciseDbContext context) : base(context)
    {
    }
}
