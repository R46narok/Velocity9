using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Dto;

namespace V9.Services.Workout.Mapping;

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