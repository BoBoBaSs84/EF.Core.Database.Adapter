using System.ComponentModel.DataAnnotations;

using Domain.Enumerators.Finance;

using MaxLength = Domain.Common.Constants.Sql.MaxLength;
using RegexPatterns = Domain.Common.Constants.RegexPatterns;

namespace Application.Contracts.Requests.Finance;

/// <summary>
/// The account create request class.
/// </summary>
public sealed class AccountCreateRequest
{
	/// <summary>
	/// The iban number.
	/// </summary>
	[Required, MaxLength(MaxLength.MAX_25), RegularExpression(RegexPatterns.IBAN)]
	public string IBAN { get; set; } = string.Empty;

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

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public CardCreateRequest[]? Cards { get; set; }
}
