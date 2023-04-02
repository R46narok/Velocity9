using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Serverless.Exercises.Queue;

namespace V9.Services.Workout.Serverless.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<ExerciseCreatedEvent, CreateExerciseCommand>()
            .ReverseMap();
    }
}