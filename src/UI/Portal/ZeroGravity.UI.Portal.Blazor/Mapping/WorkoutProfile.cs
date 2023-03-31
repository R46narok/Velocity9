using AutoMapper;
using ZeroGravity.UI.Portal.Blazor.Pages.Workout.Generate.ViewModels;
using ZeroGravity.UI.Portal.Blazor.Pages.Workout.Index.ViewModels;
using ZeroGravity.UI.Portal.Services.Workout.Requests;
using ZeroGravity.UI.Portal.Services.Workout.Views;

namespace ZeroGravity.UI.Portal.Blazor.Mapping;

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