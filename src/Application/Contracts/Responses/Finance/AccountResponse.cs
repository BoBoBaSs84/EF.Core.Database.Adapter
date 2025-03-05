using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;
using BB84.Home.Domain.Enumerators.Finance;

namespace BB84.Home.Application.Contracts.Responses.Finance;

/// <summary>
/// The account response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class AccountResponse : IdentityResponse
{
	/// <summary>
	/// The international bank account number.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public string IBAN { get; set; } = string.Empty;

	/// <summary>
	/// The type of the bank account.
	/// </summary>
	[Required]
	public AccountType Type { get; set; }

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public string Provider { get; set; } = string.Empty;

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public CardResponse[]? Cards { get; set; }
}
