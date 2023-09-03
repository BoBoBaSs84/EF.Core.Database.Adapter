using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

namespace Application.Contracts.Responses.Finance;

/// <summary>
/// The card response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponseModel"/> class.
/// </remarks>
public sealed class CardResponse : IdentityResponseModel
{
	/// <summary>
	/// The card type.
	/// </summary>
	public string CardType { get; set; } = default!;

	/// <summary>
	/// The payment card number.
	/// </summary>
	public string PAN { get; set; } = default!;

	/// <summary>
	/// The valid until property.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime ValidUntil { get; set; } = default!;
}
