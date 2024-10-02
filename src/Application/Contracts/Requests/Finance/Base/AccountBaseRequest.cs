using System.ComponentModel.DataAnnotations;

using Domain.Enumerators.Finance;

namespace Application.Contracts.Requests.Finance.Base;

/// <summary>
/// The account base reqeust class.
/// </summary>
public abstract class AccountBaseRequest
{
	/// <summary>
	/// The type of the bank account.
	/// </summary>
	[Required]
	public required AccountType Type { get; init; }

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, MaxLength(500)]
	public required string Provider { get; init; }
}
