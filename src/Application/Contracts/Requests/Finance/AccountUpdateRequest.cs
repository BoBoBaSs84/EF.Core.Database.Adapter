using System.ComponentModel.DataAnnotations;

using MaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The account update request class.
/// </summary>
public sealed class AccountUpdateRequest
{
	/// <summary>
	/// The identifier of the account.
	/// </summary>
	[Required]
	public Guid Id { get; set; }

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_500)]
	public string Provider { get; set; } = string.Empty;

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public CardUpdateRequest[]? Cards { get; set; }
}
