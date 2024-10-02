using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Finance.Base;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The account create request class.
/// </summary>
public sealed class AccountCreateRequest : AccountBaseRequest
{
	/// <summary>
	/// The iban number.
	/// </summary>
	[Required]
	public required string IBAN { get; init; }

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public CardCreateRequest[]? Cards { get; init; }
}
