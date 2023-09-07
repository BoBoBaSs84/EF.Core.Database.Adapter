using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

using Microsoft.AspNetCore.Identity;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;
using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Domain.Entities.Identity;

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
	/// The <see cref="Preferences"/> property.
	/// </summary>
	[Column(TypeName = SqlDataType.XML)]
	public string? Preferences { get; set; }

	/// <summary>
	/// The <see cref="Picture"/> property.
	/// </summary>
	public byte[]? Picture { get; set; }

	/// <summary>
	/// The <see cref="XmlPreferencesWrapper"/> property.
	/// </summary>
	[NotMapped]
	public XElement XmlPreferencesWrapper
	{
		get => Preferences is not null ? XElement.Parse(Preferences) : default!;
		set
		{
			if (value.ToString() != Preferences)
				Preferences = value.ToString();
		}
	}
}
