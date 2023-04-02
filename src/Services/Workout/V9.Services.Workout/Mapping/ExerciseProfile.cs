using AutoMapper;
using V9.Services.Workout.Commands;
using V9.Services.Workout.Data.Entities;
using V9.Services.Workout.Data.Remotes;

namespace V9.Services.Workout.Mapping;

public class ExerciseProfile : Profile
{
    public ExerciseProfile()
    {
        CreateMap<RemoteExercise, CreateExerciseCommand>()
            .ReverseMap();
        
        CreateMap<CreateExerciseCommand, Exercise>()
                    .ForMember(x => x.Targets, opt => opt.Ignore())
                    .ForMember(x => x.Author, opt => opt.Ignore())
                    .ReverseMap();

    }
}