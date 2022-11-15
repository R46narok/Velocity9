using AutoMapper;
using ZeroGravity.Services.Skeletal.Commands.Exercises.CreateExercise;
using ZeroGravity.Services.Skeletal.Commands.Exercises.DeleteExercise;
using ZeroGravity.Services.Skeletal.Commands.Exercises.UpdateExercise;
using ZeroGravity.Services.Skeletal.Data.Entities;
using ZeroGravity.Services.Skeletal.Dto;

namespace ZeroGravity.Services.Skeletal.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<CreateExerciseCommand, Exercise>()
            .ForMember(x => x.Targets, opt => opt.Ignore())
            .ForMember(x => x.Author, opt => opt.Ignore());
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<Exercise, ExerciseCreatedEvent>();
        CreateMap<Exercise, ExerciseDeletedEvent>();
        CreateMap<Exercise, ExerciseUpdatedEvent>();

    }
}