﻿using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Responses.Identity;

/// <summary>
/// The authentication response class.
/// </summary>
public sealed class AuthenticationResponse
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
