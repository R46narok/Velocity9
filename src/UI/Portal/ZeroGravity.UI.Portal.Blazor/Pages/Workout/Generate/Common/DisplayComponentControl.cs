using Microsoft.AspNetCore.Components;

namespace ZeroGravity.UI.Portal.Blazor.Pages.Workout.Generate.Common;

public class DisplayComponentControl : ComponentBase
{
    [Parameter]
    public Action? ControlRequested { get; set; }
}