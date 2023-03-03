using Domain.Common.EntityBaseTypes;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Constants;
using static Domain.Constants.Sql;

namespace Domain.Entities.Finance;

/// <summary>
/// The card entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
[Index(nameof(PAN), IsUnique = true)]
public partial class Card : AuditedModel
{
	private string pan = default!;

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public int UserId { get; set; } = default!;

	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public int AccountId { get; set; } = default!;

	/// <summary>
	/// The <see cref="CardTypeId"/> property.
	/// </summary>
	public int CardTypeId { get; set; } = default!;

	/// <summary>
	/// The <see cref="PAN"/> property.
	/// </summary>
	/// <remarks>
	/// The payment card number or <b>p</b>rimary <b>a</b>ccount <b>n</b>umber.
	/// </remarks>
	[MaxLength(MaxLength.MAX_25), Unicode(false), RegularExpression(Regex.CC)]
	public string PAN
	{
		get => pan;
		set
		{
			if (value != pan)
				pan = value.RemoveWhitespace();
		}
	}

	/// <summary>
	/// The <see cref="ValidUntil"/> property.
	/// </summary>
	[Column(TypeName = Sql.DataType.DATE)]
	public DateTime ValidUntil { get; set; } = default!;
}
