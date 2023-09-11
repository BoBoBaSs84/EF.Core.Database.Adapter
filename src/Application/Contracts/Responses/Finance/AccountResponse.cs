﻿using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

namespace Application.Contracts.Responses.Finance;

/// <summary>
/// The account response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class AccountResponse : IdentityResponse
{
	/// <summary>
	/// The international bank account number.
	/// </summary>
	[DataType(DataType.Text)]
	public string IBAN { get; set; } = string.Empty;

	/// <summary>
	/// The account provider.
	/// </summary>
	[DataType(DataType.Text)]
	public string Provider { get; set; } = string.Empty;

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public CardResponse[]? Cards { get; set; }
}
