﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Domain.Converters;
using Domain.Enumerators;

using MaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;
using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The card create request class.
/// </summary>
public sealed class CardCreateRequest
{
	/// <summary>
	/// The type of the card.
	/// </summary>
	[Required]
	public CardType CardType { get; set; }

	/// <summary>
	/// The payment card number.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_25), RegularExpression(RegexPatterns.CC)]
	public string PAN { get; set; } = string.Empty;

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required]
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime ValidUntil { get; set; }
}
