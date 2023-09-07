using System.ComponentModel.DataAnnotations;

using Application.Contracts.Responses.Base;

namespace Application.Contracts.Responses.Finance;

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
	[DataType(DataType.Date)]
	public DateTime BookingDate { get; set; } = default!;

	/// <summary>
	/// The value date of the bank transaction.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime ValueDate { get; set; } = default!;

	/// <summary>
	/// The posting text of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string PostingText { get; set; } = default!;

	/// <summary>
	/// The client beneficiary of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string ClientBeneficiary { get; set; } = default!;

	/// <summary>
	/// The purpose of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string Purpose { get; set; } = default!;

	/// <summary>
	/// The bank account number of the transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string AccountNumber { get; set; } = default!;

	/// <summary>
	/// The bank code of the transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string BankCode { get; set; } = default!;

	/// <summary>
	/// The amount in EUR of the bank transaction.
	/// </summary>
	[DataType(DataType.Currency)]
	public decimal AmountEur { get; set; } = default!;

	/// <summary>
	/// The creditor identifier of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string CreditorId { get; set; } = default!;

	/// <summary>
	/// The mandate reference of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string MandateReference { get; set; } = default!;

	/// <summary>
	/// The customer reference of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string CustomerReference { get; set; } = default!;
}
