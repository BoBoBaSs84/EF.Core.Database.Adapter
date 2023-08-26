using System.ComponentModel.DataAnnotations;

using MaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;
using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The card create request class.
/// </summary>
public sealed class CardCreateRequest
{
	/// <summary>
	/// The card type identifier.
	/// </summary>
	[Required, Range(1, int.MaxValue)]
	public int CardTypeId { get; set; } = default!;

	/// <summary>
	/// The payment card number.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_25), RegularExpression(RegexPatterns.CC)]
	public string PAN { get; set; } = default!;

	/// <summary>
	/// The valid until property.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public DateTime ValidUntil { get; set; } = default!;
}
