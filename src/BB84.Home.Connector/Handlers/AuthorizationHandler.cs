using BB84.Extensions;
using BB84.Home.Connector.Abstractions.Handlers;

namespace BB84.Home.Connector.Handlers;

/// <summary>
/// Represents a message handler that is used to add authorization headers to the request.
/// </summary>
/// <param name="tokenHandler">The token handler to use for handling tokens.</param>
internal sealed class AuthorizationHandler(ITokenHandler tokenHandler) : DelegatingHandler
{
#if NET6_0_OR_GREATER
	protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		string token = tokenHandler.GetToken();

		request.WithBearerToken(token);

		return base.Send(request, cancellationToken);
	}
#endif

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		string token = await tokenHandler.GetTokenAsync(cancellationToken)
			.ConfigureAwait(false);

		request.WithBearerToken(token);

		return await base.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);
	}
}
