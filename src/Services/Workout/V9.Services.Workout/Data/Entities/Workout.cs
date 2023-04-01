using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using V9.Domain.Entities;

namespace V9.Services.Workout.Data.Entities;

public enum WorkoutType
{
    None, Push, Pull, Legs
}

public class Workout : EntityBase<int>
{
    [Required]
    public string Name { get; set; }
    public string Notes { get; set; }
    public WorkoutType Type { get; set; }
    
    public DateTime CompletedOn { get; set; }
    public List<Set> Sets { get; set; }
    public User User { get; set; }
}