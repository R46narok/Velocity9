namespace ZeroGravity.Services.Workout.DeepLearning.Models;

public class WorkoutModelOutput
{
    public WorkoutModelOutput(List<string> Exercises, List<int> Reps)
    {
        this.Exercises = Exercises;
        this.Reps = Reps;
    }

    public WorkoutModelOutput()
    {
        
    }

    public List<string> Exercises { get; }
    public List<int> Reps { get; }
}