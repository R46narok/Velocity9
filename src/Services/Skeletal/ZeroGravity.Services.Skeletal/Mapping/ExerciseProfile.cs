using AutoMapper;
using ZeroGravity.Services.Skeletal.Commands.Exercises.CreateExercise;
using ZeroGravity.Services.Skeletal.Data.Entities;

namespace ZeroGravity.Services.Skeletal.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<CreateExerciseCommand, Exercise>()
            .ForMember(x => x.Targets, opt => opt.Ignore())
            .ForMember(x => x.Author, opt => opt.Ignore());
    }
}