using System.ComponentModel.DataAnnotations;

using BB84.EntityFrameworkCore.Models;
using BB84.Extensions;

using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Domain.Models.Finance;

/// <summary>
/// The account model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
public partial class AccountModel : AuditedModel
{
	private string _iban = default!;

	/// <summary>
	/// The <see cref="IBAN"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_25), RegularExpression(RegexPatterns.IBAN)]
	public string IBAN
	{
		get => _iban;
		set
		{
			if (value != _iban)
				_iban = value.RemoveWhitespace();
		}
	}

	/// <summary>
	/// The <see cref="Provider"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_500)]
	public string Provider { get; set; } = default!;
}
