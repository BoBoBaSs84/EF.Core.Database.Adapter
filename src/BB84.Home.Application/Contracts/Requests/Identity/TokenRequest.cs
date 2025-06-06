﻿using System.ComponentModel.DataAnnotations;

namespace BB84.Home.Application.Contracts.Requests.Identity;

/// <summary>
/// The token request class.
/// </summary>
public sealed class TokenRequest
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
}
