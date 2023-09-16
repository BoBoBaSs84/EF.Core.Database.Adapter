using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

using Domain.Extensions;
using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public partial class UserModel : IdentityUser<Guid>
{
	/// <summary>
	/// The <see cref="FirstName"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_100)]
	public string FirstName { get; set; } = default!;

	/// <summary>
	/// The <see cref="MiddleName"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_100)]
	public string? MiddleName { get; set; }

	/// <summary>
	/// The <see cref="LastName"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_100)]
	public string LastName { get; set; } = default!;

	/// <summary>
	/// The <see cref="DateOfBirth"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.DATE)]
	public DateTime? DateOfBirth { get; set; }

	/// <summary>
	/// The preferences property.
	/// </summary>
	public PreferencesModel? Preferences { get; set; }

	/// <summary>
	/// The <see cref="Picture"/> property.
	/// </summary>
	public byte[]? Picture { get; set; }
}
