namespace ZeroGravity.UI.Portal.Blazor.Services;

public class NavigationEventArgs : EventArgs
{
    public bool Main { get; set; }
}

public interface INavigationService
{
    public event EventHandler<NavigationEventArgs> NavigationChanged;
    public void Invoke(object sender, NavigationEventArgs args);
}

public class NavigationService : INavigationService
{
    public event EventHandler<NavigationEventArgs>? NavigationChanged;

    public void Invoke(object sender, NavigationEventArgs args)
    {
        NavigationChanged?.Invoke(sender, args);
    }
}