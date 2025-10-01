namespace BB84.Home.Connector.Abstractions.Handlers;

/// <summary>
/// Represents a token handler interface that is used to handle tokens in the application.
/// </summary>
public interface ITokenHandler
{
	/// <summary>
	/// Retrieves the token synchronously.
	/// </summary>
	/// <returns>The token as a string.</returns>
	string GetAccessToken();

	/// <summary>
	/// Retrieves the token asynchronously.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.
	/// The task result contains the token as a string.</returns>
	Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
