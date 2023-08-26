using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

using Microsoft.AspNetCore.Identity;

using DC = Domain.Constants.DomainConstants;

namespace Domain.Entities.Identity;

/// <inheritdoc/>
public partial class User : IdentityUser<int>
{
	/// <summary>
	/// The <see cref="FirstName"/> property.
	/// </summary>
	[MaxLength(DC.Sql.MaxLength.MAX_100)]
	public string FirstName { get; set; } = default!;

	/// <summary>
	/// The <see cref="MiddleName"/> property.
	/// </summary>
	[MaxLength(DC.Sql.MaxLength.MAX_100)]
	public string? MiddleName { get; set; } = default!;

	/// <summary>
	/// The <see cref="LastName"/> property.
	/// </summary>
	[MaxLength(DC.Sql.MaxLength.MAX_100)]
	public string LastName { get; set; } = default!;

	/// <summary>
	/// The <see cref="DateOfBirth"/> property.
	/// </summary>
	[Column(TypeName = DC.Sql.DataType.DATE)]
	public DateTime? DateOfBirth { get; set; } = default!;

	/// <summary>
	/// The <see cref="Preferences"/> property.
	/// </summary>
	[Column(TypeName = DC.Sql.DataType.XML)]
	public string? Preferences { get; set; } = default!;

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
