using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;

namespace BB84.Home.Application.Contracts.Responses.Finance;

/// <summary>
/// The bank transaction response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityResponse"/> class.
/// </remarks>
public sealed class TransactionResponse : IdentityResponse
{
	/// <summary>
	/// The booking date of the bank transaction.
	/// </summary>
	[Required, DataType(DataType.Date)]
	public required DateTime BookingDate { get; init; }

	/// <summary>
	/// The value date of the bank transaction.
	/// </summary>
	[DataType(DataType.Date)]
	public required DateTime? ValueDate { get; init; }

	/// <summary>
	/// The posting text of the bank transaction.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string PostingText { get; init; }

	/// <summary>
	/// The client beneficiary of the bank transaction.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string ClientBeneficiary { get; init; }

	/// <summary>
	/// The purpose of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Purpose { get; init; }

	/// <summary>
	/// The bank account number of the transaction.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string AccountNumber { get; init; }

	/// <summary>
	/// The bank code of the transaction.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string BankCode { get; init; }

	/// <summary>
	/// The amount in EUR of the bank transaction.
	/// </summary>
	[Required, DataType(DataType.Currency)]
	public required decimal AmountEur { get; init; }

	/// <summary>
	/// The creditor identifier of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string? CreditorId { get; init; }

	/// <summary>
	/// The mandate reference of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string? MandateReference { get; init; }

	/// <summary>
	/// The customer reference of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string? CustomerReference { get; init; }
}
