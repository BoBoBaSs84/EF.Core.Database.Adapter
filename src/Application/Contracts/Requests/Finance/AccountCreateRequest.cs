﻿using System.ComponentModel.DataAnnotations;
using MaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;
using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The account create request class.
/// </summary>
public sealed class AccountCreateRequest
{
	/// <summary>
	/// The iban number.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_25), RegularExpression(RegexPatterns.IBAN)]
	public string IBAN { get; set; } = default!;

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_500)]
	public string Provider { get; set; } = default!;

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public IEnumerable<CardCreateRequest>? Cards { get; set; }
}