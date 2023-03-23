using Domain.Common.EntityBaseTypes;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Domain.Entities.Finance;

/// <summary>
/// The account entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
// TODO: infrastructure! -> index
[Index(nameof(IBAN), IsUnique = true)]
public partial class Account : AuditedModel
{
	private string iban = default!;

	/// <summary>
	/// The <see cref="IBAN"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_25), Unicode(false), RegularExpression(RegexPatterns.IBAN)]
	public string IBAN
	{
		get => iban;
		set
		{
			if (value != iban)
				iban = value.RemoveWhitespace();
		}
	}

	/// <summary>
	/// The <see cref="Provider"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_500)]
	public string Provider { get; set; } = default!;
}
