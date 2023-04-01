namespace V9.Services.Workout.DeepLearning.Models;

public class WorkoutModelInput
{
    public WorkoutModelInput(List<string> Exercises, List<int> Reps)
    {
        this.Exercises = Exercises;
        this.Reps = Reps;
    }

    public WorkoutModelInput()
    {
        
    }

    public List<string> Exercises { get; }
    public List<int> Reps { get; }
}