using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Domain.Converters;
using Domain.Enumerators;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The card update request class.
/// </summary>
public sealed class CardUpdateRequest
{
	/// <summary>
	/// The identifier of the card.
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	/// The type of the card.
	/// </summary>
	[Required]
	public CardType CardType { get; set; }

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required]
	[DataType(DataType.Date)]
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime ValidUntil { get; set; } = default!;
}
