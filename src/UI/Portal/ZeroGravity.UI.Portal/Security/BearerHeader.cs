namespace ZeroGravity.UI.Portal.Security;

public class BearerHeader
{
    public static string Construct(string token) => "Bearer " + token;
}