using System.Net.Http.Headers;

namespace BB84.Home.Connector.Extensions;

/// <summary>
/// The extension methods for <see cref="HttpRequestMessage"/> class.
/// </summary>
internal static class HttpRequestMessageExtensions
{
	private const string AuthorizationScheme = "Bearer";

	/// <summary>
	/// Adds the authorization header to the request message.
	/// </summary>
	/// <param name="request">The request message to add the bearer token to.</param>
	/// <param name="token">The bearer token to add.</param>
	public static void AddBearerToken(this HttpRequestMessage request, string token)
	{
		if (string.IsNullOrWhiteSpace(token))
			throw new ArgumentException("Token cannot be null or empty.", nameof(token));

		request.Headers.Authorization = new AuthenticationHeaderValue(AuthorizationScheme, token);
	}
}
