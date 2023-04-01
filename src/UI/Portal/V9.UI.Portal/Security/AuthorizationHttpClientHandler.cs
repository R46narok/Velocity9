using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using V9.UI.Core.Providers;

namespace V9.UI.Portal.Security;

public class AuthorizationHttpClientHandler : DelegatingHandler
{
    private readonly TokenAuthenticationStateProvider _provider;

    public AuthorizationHttpClientHandler(TokenAuthenticationStateProvider provider)
    {
        _provider = provider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // var accessToken = await _provider.GetTokenAsync();
        // if (!string.IsNullOrEmpty(accessToken))
        // {
        //     request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        // }
        return await base.SendAsync(request, cancellationToken);
    }
}
