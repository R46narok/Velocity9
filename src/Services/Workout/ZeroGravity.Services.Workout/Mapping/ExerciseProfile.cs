using AutoMapper;
using ZeroGravity.Services.Workout.Commands;
using ZeroGravity.Services.Workout.Data.Entities;
using ZeroGravity.Services.Workout.Data.Remotes;
using ZeroGravity.Services.Workout.Handlers;

namespace ZeroGravity.Services.Workout.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<RemoteExercise, CreateExerciseCommand>()
            .ReverseMap();
        CreateMap<ExerciseCreatedEvent, CreateExerciseCommand>()
            .ReverseMap();
        
        CreateMap<CreateExerciseCommand, Exercise>()
                    .ForMember(x => x.Targets, opt => opt.Ignore())
                    .ForMember(x => x.Author, opt => opt.Ignore())
                    .ReverseMap();

    }
}