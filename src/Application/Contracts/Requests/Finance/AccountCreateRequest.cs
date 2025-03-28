﻿using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Requests.Finance.Base;

namespace BB84.Home.Application.Contracts.Requests.Finance;

/// <summary>
/// The request for creating a bank account.
/// </summary>
public sealed class AccountCreateRequest : AccountBaseRequest
{
	/// <summary>
	/// The international bank account number.
	/// </summary>
	[Required]
	public required string IBAN { get; init; }

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public CardCreateRequest[]? Cards { get; init; }
}
