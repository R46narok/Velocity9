namespace ZeroGravity.Domain.Types;

public class PipelineResult
{
    public List<string>? Details { get; init; }
    public string StatusCode { get; set; }
    public PipelineResult(IEnumerable<string>? details = null, string statusCode = "Ok")
    {
        Details = details?.ToList();
        StatusCode = statusCode;
    }

    public PipelineResult(string detail, string statusCode = "Ok")
    {
        Details = new List<string> {detail};
        StatusCode = statusCode;
    }
    
}

public class PipelineResult<TModel> : PipelineResult
{
    public TModel Result { get; set; }
    
    public PipelineResult(TModel model, IEnumerable<string>? details = null, string statusCode = "Ok") 
        : base(details, statusCode)
    {
        Result = model;
    }

    public PipelineResult(TModel model, string details = "", string statusCode = "Ok") 
        : base(details, statusCode)
    {
        Result = model;
    }
    
    public PipelineResult(TModel model) : base()
    {
        Result = model;
        StatusCode = "Ok";
        Details = null;
    }

    public PipelineResult() : base()
    {
    }
}
