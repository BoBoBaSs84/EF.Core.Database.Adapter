using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Converters;

using Domain.Enumerators.Finance;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The card update request class.
/// </summary>
public sealed class CardUpdateRequest
{
	/// <summary>
	/// The card type.
	/// </summary>
	[Required]
	public CardType Type { get; set; }

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	[JsonConverter(typeof(DateTimeJsonConverter))]
	public DateTime ValidUntil { get; set; }
}
