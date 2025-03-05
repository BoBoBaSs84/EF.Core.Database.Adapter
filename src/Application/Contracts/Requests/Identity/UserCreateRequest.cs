﻿using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Requests.Identity.Base;

namespace BB84.Home.Application.Contracts.Requests.Identity;

/// <summary>
/// The user create request class.
/// </summary>
public sealed class UserCreateRequest : UserBaseRequest
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
