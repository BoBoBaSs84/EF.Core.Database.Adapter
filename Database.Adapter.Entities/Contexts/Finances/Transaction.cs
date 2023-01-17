using Database.Adapter.Entities.BaseTypes;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.Contexts.Finances;

/// <summary>
/// The transaction entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class.
/// </remarks>
public partial class Transaction : AuditedModel
{	
	public DateTime BookingDate { get; set; } = default!;

	public DateTime ValueDate { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	public string PostingText { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	public string ClientBeneficiary { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_2048)]
	public string Purpose { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string AccountNumber { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string BankCode { get; set; } = default!;

	public decimal AmountEur { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string CreditorId { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_64)]
	public string MandateReference { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_64)]
	public string CustomerReference { get; set; } = default!;
}
