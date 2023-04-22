using AutoMapper;
using V9.UI.Portal.Blazor.Pages.Exercise.Explore.ViewModels;
using V9.UI.Portal.Blazor.Pages.Workout.Generate.ViewModels;
using V9.UI.Portal.Blazor.Pages.Workout.Index.ViewModels;
using V9.UI.Portal.Services.Skeletal.Requests;
using V9.UI.Portal.Services.Workout.Requests;
using V9.UI.Portal.Services.Workout.Views;

namespace V9.UI.Portal.Blazor.Mapping;

public class WorkoutProfile : Profile
{
    public WorkoutProfile()
    {
        CreateMap<PredictWorkoutView, PredictedWorkoutViewModel>()
            .ReverseMap();

        CreateMap<UpdateSetViewModel, UpdateSetRequest>()
            .ReverseMap();

        CreateMap<CreateExerciseViewModel, CreateExerciseRequest>()
            .ForMember(x => x.ExecutionSteps,
                opt => opt.MapFrom(t => t.ExecutionSteps.Split('.', StringSplitOptions.RemoveEmptyEntries).ToArray()));
    }
}