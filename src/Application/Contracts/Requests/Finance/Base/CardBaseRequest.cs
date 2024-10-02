using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Converters;

using Domain.Enumerators.Finance;

namespace Application.Contracts.Requests.Finance.Base;

/// <summary>
/// The card base request class.
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
	[JsonConverter(typeof(DateTimeJsonConverter))]
	public required DateTime ValidUntil { get; init; }
}
