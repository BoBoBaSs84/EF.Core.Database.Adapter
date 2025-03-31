using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Requests.Finance.Base;

namespace BB84.Home.Application.Contracts.Requests.Finance;

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
