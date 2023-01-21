using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Database.Adapter.Entities.Constants.Sql;

namespace Database.Adapter.Entities.Contexts.Finances;

/// <summary>
/// The transaction entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class.
/// </remarks>
public partial class Transaction : AuditedModel
{
	/// <summary>
	/// The <see cref="BookingDate"/> property.
	/// </summary>
	[Column(TypeName = Constants.Sql.DataType.DATE)]
	public DateTime BookingDate { get; set; } = default!;
	/// <summary>
	/// The <see cref="ValueDate"/> property.
	/// </summary>
	[Column(TypeName = Constants.Sql.DataType.DATE)]
	public DateTime ValueDate { get; set; } = default!;
	/// <summary>
	/// The <see cref="PostingText"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_100)]
	public string PostingText { get; set; } = default!;
	/// <summary>
	/// The <see cref="ClientBeneficiary"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_250)]
	public string ClientBeneficiary { get; set; } = default!;
	/// <summary>
	/// The <see cref="Purpose"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_4000)]
	public string Purpose { get; set; } = default!;
	/// <summary>
	/// The <see cref="AccountNumber"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_25), Unicode(false)]
	public string AccountNumber { get; set; } = default!;
	/// <summary>
	/// The <see cref="BankCode"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_25), Unicode(false)]
	public string BankCode { get; set; } = default!;
	/// <summary>
	/// The <see cref="AmountEur"/> property.
	/// </summary>
	[Column(TypeName = Constants.Sql.DataType.MONEY)]
	public decimal AmountEur { get; set; } = default!;
	/// <summary>
	/// The <see cref="CreditorId"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_25)]
	public string CreditorId { get; set; } = default!;
	/// <summary>
	/// The <see cref="MandateReference"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_50)]
	public string MandateReference { get; set; } = default!;
	/// <summary>
	/// The <see cref="CustomerReference"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_50)]
	public string CustomerReference { get; set; } = default!;
}
