using AutoMapper;
using V9.UI.Portal.Blazor.Pages.Workout.Generate.ViewModels;
using V9.UI.Portal.Blazor.Pages.Workout.Index.ViewModels;
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
    }
}