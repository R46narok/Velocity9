namespace ZeroGravity.Application;

public static class DetailsMessage
{
    public static string For(string status, string? name = null)
    {
        switch (status)
        {
            case StatusCode.Ok: return $"Action completed successfully.";
            case StatusCode.Created: return $"{name} created successfully.";
            case StatusCode.Fetched: return $"{name} successfully retrieved from the database.";
            case StatusCode.NotFound: return $"{name} does not exist in the database.";
            case StatusCode.BadRequest: return $"Request was not formatted properly.";
            case StatusCode.Unauthorized: return $"Not enough privileges.";
        }

        throw new Exception();
    }
}