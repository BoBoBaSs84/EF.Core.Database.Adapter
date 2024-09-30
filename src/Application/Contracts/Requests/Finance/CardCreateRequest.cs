using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Converters;

using Domain.Enumerators.Finance;

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
	public CardType Type { get; set; }

	/// <summary>
	/// The payment card number.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_25), RegularExpression(RegexPatterns.PAN)]
	public string PAN { get; set; } = string.Empty;

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	[JsonConverter(typeof(DateTimeJsonConverter))]
	public DateTime ValidUntil { get; set; }
}
