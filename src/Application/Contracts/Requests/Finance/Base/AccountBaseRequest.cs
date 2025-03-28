﻿using System.ComponentModel.DataAnnotations;

using BB84.Home.Domain.Enumerators.Finance;

namespace BB84.Home.Application.Contracts.Requests.Finance.Base;

/// <summary>
/// The base request for creating or updating a bank account.
/// </summary>
public abstract class AccountBaseRequest
{
	/// <summary>
	/// The type of the bank account.
	/// </summary>
	[Required]
	public required AccountType Type { get; init; }

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, MaxLength(500)]
	public required string Provider { get; init; }
}
