using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The card update request class.
/// </summary>
public sealed class CardUpdateRequest
{
	/// <summary>
	/// The identifier of the card.
	/// </summary>
	[Required, Range(1, int.MaxValue)]
	public int Id { get; set; } = default!;

	/// <summary>
	/// The card type identifier.
	/// </summary>
	[Required, Range(1, int.MaxValue)]
	public int CardTypeId { get; set; } = default!;

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime ValidUntil { get; set; } = default!;
}
