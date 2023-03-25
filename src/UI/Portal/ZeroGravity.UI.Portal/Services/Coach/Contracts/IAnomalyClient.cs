using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ZeroGravity.UI.Portal.Services.Coach.Contracts;

public interface IAnomalyClient
{
    [Multipart]
    [Post(Endpoints.Anomaly)]
    public Task<IApiResponse<Stream>> PredictAsync([AliasAs("file")] StreamPart streamPart);
}