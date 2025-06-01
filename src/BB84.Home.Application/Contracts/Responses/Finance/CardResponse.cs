using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;
using BB84.Home.Domain.Enumerators.Finance;

namespace BB84.Home.Application.Contracts.Responses.Finance;

/// <summary>
/// The bank card response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class CardResponse : IdentityResponse
{
	/// <summary>
	/// The payment card number.
	/// </summary>
	[Required, DataType(DataType.CreditCard)]
	public required string PAN { get; init; }

	/// <summary>
	/// The card type.
	/// </summary>
	[Required]
	public required CardType Type { get; init; }

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public required DateTime ValidUntil { get; init; }
}
