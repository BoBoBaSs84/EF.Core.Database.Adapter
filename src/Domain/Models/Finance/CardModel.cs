using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using BB84.EntityFrameworkCore.Models;
using BB84.Extensions;

using Domain.Enumerators;

using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;
using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Domain.Models.Finance;

/// <summary>
/// The card model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
public partial class CardModel : AuditedModel
{
	private string _pan = default!;

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; } = default!;

	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public Guid AccountId { get; set; } = default!;

	/// <summary>
	/// The card type property.
	/// </summary>
	public CardType CardType { get; set; }

	/// <summary>
	/// The <see cref="PAN"/> property.
	/// </summary>
	/// <remarks>
	/// The payment card number or <b>p</b>rimary <b>a</b>ccount <b>n</b>umber.
	/// </remarks>
	[MaxLength(SqlMaxLength.MAX_25), RegularExpression(RegexPatterns.PAN)]
	public string PAN
	{
		get => _pan;
		set
		{
			if (value != _pan)
				_pan = value.RemoveWhitespace();
		}
	}

	/// <summary>
	/// The <see cref="ValidUntil"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.DATE)]
	public DateTime ValidUntil { get; set; } = default!;
}
