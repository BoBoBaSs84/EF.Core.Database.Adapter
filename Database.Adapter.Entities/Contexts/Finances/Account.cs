using Database.Adapter.Entities.Extensions;
using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Constants;
using static Database.Adapter.Entities.Constants.Sql;

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
	private string iBAN = default!;

	/// <summary>
	/// The <see cref="IBAN"/> property.
	/// </summary>
	[StringLength(20), RegularExpression(Regex.IBAN), Unicode(false)]
	public string IBAN { get => iBAN; set => iBAN = value.RemoveWhitespace(); }
	/// <summary>
	/// The <see cref="Provider"/> property.
	/// </summary>
	[StringLength(StringLength.MAX_LENGHT_256)]
	public string Provider { get; set; } = default!;
}
