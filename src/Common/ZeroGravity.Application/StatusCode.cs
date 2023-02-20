﻿using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Domain.Types;

namespace ZeroGravity.Application;

public static class StatusCode
{
    public const string Ok = "Ok";
    public const string Created = "Created";
    public const string NotFound = "NotFound";
    public const string BadRequest = "BadRequest";
    public const string Unauthorized = "Unauthorized";
    public const string Fetched = "Fetched";
    
    public static ObjectResult ToObjectResult(CqrsResult response)
    {
        return Impl(response.StatusCode, response);
    }
    
    public static ObjectResult ToObjectResult<T>(CqrsResult<T> response)
    {
        return Impl(response.StatusCode, response);
    }

    private static ObjectResult Impl(string status, object model)
    {
         switch (status)
         {
             case Ok: return new OkObjectResult(model);
             case Created: return new CreatedResult("/api", model);
             case Fetched: return new OkObjectResult(model);
             case NotFound: return new NotFoundObjectResult(model);
             case BadRequest: return new BadRequestObjectResult(model);
             case Unauthorized: return new UnauthorizedObjectResult(model);
         }
        
         throw new Exception();
    }
}