using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.Contexts.Finances;

/// <summary>
/// The account entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class.
/// </remarks>
[Index(nameof(IBAN), IsUnique = true)]
public partial class Account : AuditedModel
{
	/// <summary>
	/// The <see cref="IBAN"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string IBAN { get; set; } = default!;
	/// <summary>
	/// The <see cref="Provider"/> property.
	/// </summary>
	[StringLength(SqlStringLength.MAX_LENGHT_64)]
	public string Provider { get; set; } = default!;
}
