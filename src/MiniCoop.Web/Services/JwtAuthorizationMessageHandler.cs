using MiniCoop.Web.Services;
using System.Net.Http.Headers;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly TokenStore _store;

    public JwtAuthorizationMessageHandler(TokenStore store)
    {
        _store = store;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(_store.Token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _store.Token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}