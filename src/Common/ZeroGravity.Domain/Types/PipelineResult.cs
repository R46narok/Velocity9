namespace ZeroGravity.Domain.Types;

public class CqrsResult
{
    public List<string>? Details { get; init; }
    public string StatusCode { get; set; }
    public CqrsResult(IEnumerable<string>? details = null, string statusCode = "Ok")
    {
        Details = details?.ToList();
        StatusCode = statusCode;
    }

    public CqrsResult(string detail, string statusCode = "Ok")
    {
        Details = new List<string> {detail};
        StatusCode = statusCode;
    }
    
}

public class CqrsResult<TModel> : CqrsResult
{
    public TModel Result { get; set; }
    
    public CqrsResult(TModel model, IEnumerable<string>? details = null, string statusCode = "Ok") 
        : base(details, statusCode)
    {
        Result = model;
    }

    public CqrsResult(TModel model, string details = "", string statusCode = "Ok") 
        : base(details, statusCode)
    {
        Result = model;
    }
    
    public CqrsResult(TModel model) : base()
    {
        Result = model;
        StatusCode = "Ok";
        Details = null;
    }

    public CqrsResult() : base()
    {
    }
}
