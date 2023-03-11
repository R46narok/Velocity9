using AutoMapper;
using ZeroGravity.UI.Portal.Blazor.Pages.Workout.Generate.ViewModels;
using ZeroGravity.UI.Portal.Services.Workout.Views;

namespace ZeroGravity.UI.Portal.Blazor.Mapping;

public class WorkoutProfile : Profile
{
    public WorkoutProfile()
    {
        CreateMap<PredictWorkoutView, PredictedWorkoutViewModel>()
            .ReverseMap();
    }
}