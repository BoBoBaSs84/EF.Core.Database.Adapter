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
	public required string IBAN { get; init; }

	/// <summary>
	/// The type of the bank account.
	/// </summary>
	[Required]
	public required AccountType Type { get; init; }

	/// <summary>
	/// The account provider.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string Provider { get; init; }

	/// <summary>
	/// The cards belonging to this account.
	/// </summary>
	public CardResponse[]? Cards { get; init; }
}
