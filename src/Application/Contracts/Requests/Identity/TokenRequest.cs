using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Identity;

/// <summary>
/// The token request class.
/// </summary>
public sealed class TokenRequest
{
	/// <summary>
	/// The application user access token.
	/// </summary>
	[Required]
	public required string AccessToken { get; init; }

	/// <summary>
	/// The application user refresh token.
	/// </summary>
	[Required]
	public required string RefreshToken { get; init; }
}
