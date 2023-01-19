﻿using Database.Adapter.Entities.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
	/// <summary>
	/// The <see cref="BookingDate"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.DATE)]
	public DateTime BookingDate { get; set; } = default!;
	/// <summary>
	/// The <see cref="ValueDate"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.DATE)]
	public DateTime ValueDate { get; set; } = default!;
	/// <summary>
	/// The <see cref="PostingText"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	public string PostingText { get; set; } = default!;
	/// <summary>
	/// The <see cref="ClientBeneficiary"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_128)]
	public string ClientBeneficiary { get; set; } = default!;
	/// <summary>
	/// The <see cref="Purpose"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_2048)]
	public string Purpose { get; set; } = default!;
	/// <summary>
	/// The <see cref="AccountNumber"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string AccountNumber { get; set; } = default!;
	/// <summary>
	/// The <see cref="BankCode"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string BankCode { get; set; } = default!;
	/// <summary>
	/// The <see cref="AmountEur"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.MONEY)]
	public decimal AmountEur { get; set; } = default!;
	/// <summary>
	/// The <see cref="CreditorId"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string CreditorId { get; set; } = default!;
	/// <summary>
	/// The <see cref="MandateReference"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_64)]
	public string MandateReference { get; set; } = default!;
	/// <summary>
	/// The <see cref="CustomerReference"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_64)]
	public string CustomerReference { get; set; } = default!;
}
