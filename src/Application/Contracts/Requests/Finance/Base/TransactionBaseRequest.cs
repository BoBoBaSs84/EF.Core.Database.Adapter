using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Application.Converters;

namespace Application.Contracts.Requests.Finance.Base;

/// <summary>
/// The base request for creating and updating a transaction.
/// </summary>
public abstract class TransactionBaseRequest
{
	/// <summary>
	/// The booking date of the bank transaction.
	/// </summary>
	[Required]
	[JsonConverter(typeof(DateTimeJsonConverter))]
	public required DateTime BookingDate { get; init; }

	/// <summary>
	/// The value date of the bank transaction.
	/// </summary>	
	[JsonConverter(typeof(NullableDateTimeJsonConverter))]
	public DateTime? ValueDate { get; init; }

	/// <summary>
	/// The posting text of the bank transaction.
	/// </summary>
	[Required, MaxLength(100)]
	public required string PostingText { get; init; }

	/// <summary>
	/// The client beneficiary of the bank transaction.
	/// </summary>
	[Required, MaxLength(250)]
	public required string ClientBeneficiary { get; init; }

	/// <summary>
	/// The purpose of the bank transaction.
	/// </summary>
	[MaxLength(4000)]
	public string? Purpose { get; init; }

	/// <summary>
	/// The bank account number of the transaction.
	/// </summary>
	[Required, MaxLength(25)]
	public required string AccountNumber { get; init; }

	/// <summary>
	/// The bank code of the transaction.
	/// </summary>
	[Required, MaxLength(25)]
	public required string BankCode { get; init; }

	/// <summary>
	/// The amount in EUR of the bank transaction.
	/// </summary>
	[Required]
	public required decimal AmountEur { get; init; }

	/// <summary>
	/// The creditor identifier of the bank transaction.
	/// </summary>
	[MaxLength(25)]
	public string? CreditorId { get; init; }

	/// <summary>
	/// The mandate reference of the bank transaction.
	/// </summary>
	[MaxLength(50)]
	public string? MandateReference { get; init; }

	/// <summary>
	/// The customer reference of the bank transaction.
	/// </summary>
	[MaxLength(50)]
	public string? CustomerReference { get; init; }
}
