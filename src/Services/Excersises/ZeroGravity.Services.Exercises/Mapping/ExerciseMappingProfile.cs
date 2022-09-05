using AutoMapper;
using ZeroGravity.Services.Exercises.Commands.Exercises.CreateExercise;
using ZeroGravity.Services.Exercises.Data.Entities;
using ZeroGravity.Services.Exercises.Dto;

namespace ZeroGravity.Services.Exercises.Mapping;

public class ExerciseMappingProfile : Profile
{
    public ExerciseMappingProfile()
    {
        CreateMap<CreateExerciseCommand, Exercise>();
        CreateMap<Exercise, ExerciseCreatedEvent>();
        CreateMap<Exercise, ExerciseDto>();
    }
}