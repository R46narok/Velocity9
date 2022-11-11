using ZeroGravity.Services.Muscles.Data.Entities;

namespace ZeroGravity.Services.Muscles.Dto;

public class FiberDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public MotorUnitType MotorUnitType { get; set; }
    public TwitchSpeed TwitchSpeed { get; set; }
    public TwitchForce TwitchForce { get; set; }
    public ResistanceToFatigue ResistanceToFatigue { get; set; }
}