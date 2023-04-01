using AutoMapper;
using V9.Services.Skeletal.Commands.Exercises.CreateExercise;
using V9.Services.Skeletal.Commands.Exercises.DeleteExercise;
using V9.Services.Skeletal.Commands.Exercises.UpdateExercise;
using V9.Services.Skeletal.Data.Entities;
using V9.Services.Skeletal.Dto;

namespace V9.Services.Skeletal.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<CreateExerciseCommand, Exercise>()
            .ForMember(x => x.Targets, opt => opt.Ignore())
            .ForMember(x => x.Author, opt => opt.Ignore());

        CreateMap<Exercise, ExerciseDto>()
            .ForMember(x => x.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
            .ForMember(x => x.TargetNames, opt => opt.MapFrom(t => t.Targets.Select(y => y.Name).ToList()));
        
        CreateMap<Exercise, ExerciseCreatedEvent>()
            .ForMember(x => x.AuthorName, opt => opt.MapFrom(t => t.Author.UserName))
            .ForMember(x => x.TargetNames, opt => opt.MapFrom(t => t.Targets.Select(y => y.Name).ToList()));
        
        CreateMap<Exercise, ExerciseDeletedEvent>();
        CreateMap<Exercise, ExerciseUpdatedEvent>();

    }
}