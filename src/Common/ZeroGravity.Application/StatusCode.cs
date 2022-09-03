using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Domain.Types;

namespace ZeroGravity.Application;

public static class StatusCode
{
    public const string Ok = "Ok";
    public const string NotFound = "NotFound";
    public const string BadRequest = "BadRequest";
    public const string Unauthorized = "Unauthorized";

    public static ObjectResult ToObjectResult(ApiResponse response)
    {
        return Impl(response.StatusCode, response);
    }
    
    public static ObjectResult ToObjectResult<T>(ApiResponse<T> response)
    {
        return Impl(response.StatusCode, response);
    }

    private static ObjectResult Impl(string status, object model)
    {
         switch (status)
         {
             case Ok: return new OkObjectResult(model);
             case NotFound: return new NotFoundObjectResult(model);
             case BadRequest: return new BadRequestObjectResult(model);
             case Unauthorized: return new UnauthorizedObjectResult(model);
         }
        
         throw new Exception();
    }
}