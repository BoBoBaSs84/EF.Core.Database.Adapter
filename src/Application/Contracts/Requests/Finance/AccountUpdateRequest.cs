using System.ComponentModel.DataAnnotations;

using MaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The account update request class.
/// </summary>
public sealed class AccountUpdateRequest
{
	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_500)]
	public string Provider { get; set; } = string.Empty;
}
