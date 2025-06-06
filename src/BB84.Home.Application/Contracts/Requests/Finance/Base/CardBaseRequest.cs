﻿using System.ComponentModel.DataAnnotations;

using BB84.Home.Domain.Enumerators.Finance;

namespace BB84.Home.Application.Contracts.Requests.Finance.Base;

/// <summary>
/// The base request for creating or updating a bank card.
/// </summary>
public abstract class CardBaseRequest
{
	/// <summary>
	/// The card type.
	/// </summary>
	[Required]
	public required CardType Type { get; init; }

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required]
	[DataType(DataType.Date)]
	public required DateTime ValidUntil { get; init; }
}
