using Microsoft.AspNetCore.Mvc;
using Refit;

namespace V9.UI.Portal.Services.Coach.Contracts;

public interface IAnomalyClient
{
    [Multipart]
    [Post(Endpoints.Anomaly)]
    public Task<IApiResponse<Stream>> PredictAsync([AliasAs("file")] StreamPart streamPart);
}