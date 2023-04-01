using V9.UI.Portal.Blazor.Pages.Workout.Generate.Enums;

namespace V9.UI.Portal.Blazor.Pages.Workout.Generate.Extensions;

internal static class DisplayComponentExtensions
{
    public static DisplayComponent Component(this int component)
    {
        return (DisplayComponent) component;
    }
}