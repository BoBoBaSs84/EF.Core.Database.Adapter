using System.ComponentModel.DataAnnotations;

using Domain.Enumerators.Finance;

using MaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The account update request class.
/// </summary>
public sealed class AccountUpdateRequest
{
	/// <summary>
	/// The type of the bank account.
	/// </summary>
	[Required]
	public AccountType Type { get; set; }

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_500)]
	public string Provider { get; set; } = string.Empty;
}
