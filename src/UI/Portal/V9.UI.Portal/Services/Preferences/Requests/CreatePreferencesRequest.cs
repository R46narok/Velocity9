namespace V9.UI.Portal.Services.Preferences.Requests;

public class CreatePreferencesRequest
{
    public double ExerciseRestTime { get; set; }
    public double SetRestTime { get; set; }

    public string UserName => string.Empty;
}