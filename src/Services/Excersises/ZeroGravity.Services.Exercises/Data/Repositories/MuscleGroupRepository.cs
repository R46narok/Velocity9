using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Exercises.Data.Entities;

namespace ZeroGravity.Services.Exercises.Data.Repositories;

public interface IMuscleGroupRepository : IRepository<MuscleGroup, int>
{
    
}

public class MuscleGroupRepository : RepositoryBase<MuscleGroup, int, ExerciseDbContext>, IMuscleGroupRepository
{
    public MuscleGroupRepository(ExerciseDbContext context) : base(context)
    {
    }
    
    
}