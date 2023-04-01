using V9.Domain.Entities;

namespace V9.Services.Skeletal.Data.Entities;

public enum TwitchSpeed
{
    Slow, Fast    
}

public enum TwitchForce
{
    Small, Medium, Large
}

public enum ResistanceToFatigue
{
    Low, Medium, High
}

public enum MotorUnitType
{
    SlowOxidative,
    FastOxidative,
    FastGlycolytic
}

public class Fiber : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public MotorUnitType MotorUnitType { get; set; }
    public TwitchSpeed TwitchSpeed { get; set; }
    public TwitchForce TwitchForce { get; set; }
    public ResistanceToFatigue ResistanceToFatigue { get; set; }
}