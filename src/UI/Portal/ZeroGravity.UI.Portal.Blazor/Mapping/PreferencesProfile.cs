using AutoMapper;
using ZeroGravity.UI.Portal.Blazor.Pages.Workout.Index.ViewModels;
using ZeroGravity.UI.Portal.Services.Preferences.Views;

namespace ZeroGravity.UI.Portal.Blazor.Mapping;

public class PreferencesProfile : Profile
{
    public PreferencesProfile()
    {
        CreateMap<PreferencesView, PreferencesViewModel>();
    }
}