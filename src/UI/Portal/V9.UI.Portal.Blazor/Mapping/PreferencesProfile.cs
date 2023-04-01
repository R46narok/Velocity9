using AutoMapper;
using V9.UI.Portal.Blazor.Pages.Workout.Index.ViewModels;
using V9.UI.Portal.Services.Preferences.Views;

namespace V9.UI.Portal.Blazor.Mapping;

public class PreferencesProfile : Profile
{
    public PreferencesProfile()
    {
        CreateMap<PreferencesView, PreferencesViewModel>();
    }
}