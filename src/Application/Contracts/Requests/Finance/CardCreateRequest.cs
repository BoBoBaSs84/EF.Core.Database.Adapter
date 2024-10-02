using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Finance.Base;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The card create request class.
/// </summary>
public sealed class CardCreateRequest : CardBaseRequest
{
	/// <summary>
	/// The payment card number.
	/// </summary>
	[Required]
	public required string PAN { get; init; }
}
