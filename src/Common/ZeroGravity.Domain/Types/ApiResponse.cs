namespace ZeroGravity.Domain.Types;

public class ApiResponse
{
    public List<string>? Details { get; init; }
    public string StatusCode { get; set; }
    public ApiResponse(IEnumerable<string>? details = null, string statusCode = "Ok")
    {
        Details = details?.ToList();
        StatusCode = statusCode;
    }

    public ApiResponse(string detail, string statusCode = "Ok")
    {
        Details = new List<string> {detail};
        StatusCode = statusCode;
    }
    
}

public class ApiResponse<TModel> : ApiResponse
{
    public TModel Result { get; set; }
    
    public ApiResponse(TModel model, IEnumerable<string>? details = null, string statusCode = "Ok") 
        : base(details, statusCode)
    {
        Result = model;
    }

    public ApiResponse(TModel model, string details = "", string statusCode = "Ok") 
        : base(details, statusCode)
    {
        Result = model;
    }
    
    public ApiResponse(TModel model) : base()
    {
        Result = model;
        StatusCode = "Ok";
        Details = null;
    }

    public ApiResponse() : base()
    {
    }
}
