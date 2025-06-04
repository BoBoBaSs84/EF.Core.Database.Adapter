using System.ComponentModel.DataAnnotations;

namespace BB84.Home.Application.Contracts.Responses.Identity;

/// <summary>
/// The authentication response class.
/// </summary>
public sealed class AuthenticationResponse
{
	/// <summary>
	/// The application user access token.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string AccessToken { get; init; }

	/// <summary>
	/// The application user refresh token.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string RefreshToken { get; init; }

	/// <summary>
	/// The application user access token expiration date.
	/// </summary>
	[Required, DataType(DataType.DateTime)]
	public required DateTimeOffset AccessTokenExpiration { get; init; }
}
