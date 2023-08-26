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
	[Required, Range(1, int.MaxValue)]
	public int Id { get; set; } = default!;

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_500)]
	public string Provider { get; set; } = default!;

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public IEnumerable<CardUpdateRequest>? Cards { get; set; }
}
