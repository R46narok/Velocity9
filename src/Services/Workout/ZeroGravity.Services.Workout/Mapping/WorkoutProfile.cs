using AutoMapper;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Dto;

namespace ZeroGravity.Services.Workout.Mapping;

public class WorkoutProfile : Profile
{
    public WorkoutProfile()
    {
        CreateMap<CreateWorkoutCommand, Data.Entities.Workout>()
            .ForMember(x => x.Name, opt => opt.MapFrom(t => t.WorkoutName));
        CreateMap<Data.Entities.Workout, WorkoutCreatedEvent>();
        CreateMap<Data.Entities.Workout, WorkoutUpdatedEvent>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(t => t.User.UserName));
        CreateMap<Data.Entities.Workout, WorkoutDto>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(t => t.User.UserName))
            .ReverseMap();

        CreateMap<Data.Entities.Workout, WorkoutDeletedEvent>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(t => t.User.UserName));
    }
}