using Microsoft.AspNetCore.Components;

namespace V9.UI.Portal.Blazor.Pages.Workout.Generate.Common;

public class DisplayComponentControl : ComponentBase
{
    [Parameter]
    public Action? ControlRequested { get; set; }
}