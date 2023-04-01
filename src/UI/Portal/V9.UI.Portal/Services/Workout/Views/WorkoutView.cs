namespace V9.UI.Portal.Services.Workout.Views;


public enum WorkoutType
{
    None, Push, Pull, Legs
}

public class WorkoutView
{
    public string Name { get; set; }
    public string Notes { get; set; }
    // public WorkoutType Type { get; set; }

    public DateTime CompletedOn { get; set; }
    public string UserName { get; set; }
    public List<SetView> Sets { get; set; }
}