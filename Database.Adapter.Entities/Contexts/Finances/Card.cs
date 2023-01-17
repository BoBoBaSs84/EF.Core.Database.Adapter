using Database.Adapter.Entities.BaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.Contexts.Finances;

/// <summary>
/// The card entity class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="AuditedModel"/> class.
/// </remarks>
[Index(nameof(Number), IsUnique = true)]
public partial class Card : AuditedModel
{
	/// <summary>The <see cref="UserId"/> property.</summary>
	public int UserId { get; set; } = default!;
	/// <summary>The <see cref="AccountId"/> property.</summary>
	public int AccountId { get; set; } = default!;
	/// <summary>The <see cref="CardTypeId"/> property.</summary>
	public int CardTypeId { get; set; } = default!;

	[StringLength(SqlStringLength.MAX_LENGHT_32)]
	public string Number { get; set; } = default!;

	[Column(TypeName = SqlDataType.DATE)]
	public DateTime ValidUntil { get; set; } = default!;
}
