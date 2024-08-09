using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

using Domain.Enumerators.Finance;

namespace Application.Contracts.Responses.Finance;

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
	public string PAN { get; set; } = string.Empty;

	/// <summary>
	/// The card type.
	/// </summary>
	[Required]
	public CardType Type { get; set; }

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime ValidUntil { get; set; }
}
