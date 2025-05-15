using System.Net.Http.Headers;

using BB84.Home.Connector.Abstractions.Handlers;

namespace BB84.Home.Connector.Handlers;

/// <summary>
/// Represents a message handler that is used to add authorization headers to the request.
/// </summary>
/// <param name="tokenHandler">The token handler to use for handling tokens.</param>
internal sealed class AuthorizationHandler(ITokenHandler tokenHandler) : DelegatingHandler
{
	/// <inheritdoc/>
	protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		string token = tokenHandler.GetToken();

		AddAuthorizationHeader(request, token);

		return base.Send(request, cancellationToken);
	}

	/// <inheritdoc/>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		string token = await tokenHandler.GetTokenAsync(cancellationToken)
			.ConfigureAwait(false);

		AddAuthorizationHeader(request, token);

		return await base.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);
	}

	private static void AddAuthorizationHeader(HttpRequestMessage request, string token)
		=> request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
}
