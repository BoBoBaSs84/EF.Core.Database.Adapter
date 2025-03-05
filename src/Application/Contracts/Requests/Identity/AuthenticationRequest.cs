using System.ComponentModel.DataAnnotations;

namespace BB84.Home.Application.Contracts.Requests.Identity;

/// <summary>
/// The authentication request class.
/// </summary>
public sealed class AuthenticationRequest
{
	/// <summary>
	/// The user name of the user.
	/// </summary>
	[Required]
	public required string UserName { get; init; }

	/// <summary>
	/// The password of the user.
	/// </summary>
	[Required]
	public required string Password { get; init; }
}
