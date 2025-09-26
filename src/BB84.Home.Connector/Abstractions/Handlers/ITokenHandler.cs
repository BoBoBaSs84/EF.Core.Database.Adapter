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

	/// <summary>
	/// Retrieves the refresh token.
	/// </summary>
	/// <returns>The refresh token as a string.</returns>
	string GetRefreshToken();

	/// <summary>
	/// Retrieves the refresh token asynchronously.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.
	/// The task result contains the refresh token as a string.</returns>
	Task<string> GetRefreshTokenAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the access token expiration time.
	/// </summary>
	/// <returns>The expiration time of the access token.</returns>
	DateTimeOffset GetAccessTokenExpiration();

	/// <summary>
	/// Retrieves the access token expiration time asynchronously.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.
	/// The task result contains the expiration time of the access token.</returns>
	Task<DateTimeOffset> GetAccessTokenExpirationAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Sets the access token.
	/// </summary>
	/// <param name="token">The access token to set.</param>
	void SetAccessToken(string token);

	/// <summary>
	/// Sets the access token asynchronously.
	/// </summary>
	/// <param name="token">The access token to set.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task SetAccessTokenAsync(string token, CancellationToken cancellationToken = default);

	/// <summary>
	/// Sets the refresh token.
	/// </summary>
	/// <param name="token">The refresh token to set.</param>
	void SetRefreshToken(string token);

	/// <summary>
	/// Sets the refresh token asynchronously.
	/// </summary>
	/// <param name="token">The refresh token to set.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task SetRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
}
