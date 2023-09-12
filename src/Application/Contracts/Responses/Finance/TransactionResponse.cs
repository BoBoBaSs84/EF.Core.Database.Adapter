using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Contracts.Responses.Base;

using Domain.Converters;

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
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime BookingDate { get; set; }

	/// <summary>
	/// The value date of the bank transaction.
	/// </summary>
	[DataType(DataType.Date)]
	[JsonConverter(typeof(DateJsonConverter))]
	public DateTime ValueDate { get; set; }

	/// <summary>
	/// The posting text of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string PostingText { get; set; } = string.Empty;

	/// <summary>
	/// The client beneficiary of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string ClientBeneficiary { get; set; } = string.Empty;

	/// <summary>
	/// The purpose of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string Purpose { get; set; } = string.Empty;

	/// <summary>
	/// The bank account number of the transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string AccountNumber { get; set; } = string.Empty;

	/// <summary>
	/// The bank code of the transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string BankCode { get; set; } = string.Empty;

	/// <summary>
	/// The amount in EUR of the bank transaction.
	/// </summary>
	[DataType(DataType.Currency)]
	public decimal AmountEur { get; set; }

	/// <summary>
	/// The creditor identifier of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string CreditorId { get; set; } = string.Empty;

	/// <summary>
	/// The mandate reference of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string MandateReference { get; set; } = string.Empty;

	/// <summary>
	/// The customer reference of the bank transaction.
	/// </summary>
	[DataType(DataType.Text)]
	public string CustomerReference { get; set; } = string.Empty;
}
