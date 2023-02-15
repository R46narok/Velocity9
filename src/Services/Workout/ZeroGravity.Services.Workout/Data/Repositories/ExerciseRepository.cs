using ZeroGravity.Application.Interfaces;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Persistence;

namespace ZeroGravity.Services.Workout.Data.Repositories;

public interface IExerciseRepository : IRepository<Exercise, int>
{
    
}

public class ExerciseRepository : RepositoryBase<Exercise, int, WorkoutDbContext>, IExerciseRepository
{
    public ExerciseRepository(WorkoutDbContext context) : base(context)
    {
    }
}