using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Finance.Base;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The request for creating a bank card.
/// </summary>
public sealed class CardCreateRequest : CardBaseRequest
{
	/// <summary>
	/// The permanent account number.
	/// </summary>
	[Required]
	public required string PAN { get; init; }
}
