using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static Database.Adapter.Entities.Constants.Sql;

namespace Database.Adapter.Entities.Contexts.Authentication;

/// <inheritdoc/>
public partial class User : IdentityUser<int>
{
	/// <summary>
	/// The <see cref="FirstName"/> property.
	/// </summary>
	[MaxLength(StringLength.MAX_LENGHT_64)]
	public string FirstName { get; set; } = default!;
	/// <summary>
	/// The <see cref="MiddleName"/> property.
	/// </summary>
	[MaxLength(StringLength.MAX_LENGHT_64)]
	public string? MiddleName { get; set; } = default!;
	/// <summary>
	/// The <see cref="LastName"/> property.
	/// </summary>
	[MaxLength(StringLength.MAX_LENGHT_64)]
	public string LastName { get; set; } = default!;
	/// <summary>
	/// The <see cref="DateOfBirth"/> property.
	/// </summary>
	[Column(TypeName = Constants.Sql.DataType.DATE)]
	public DateTime? DateOfBirth { get; set; } = default!;
	/// <summary>
	/// The <see cref="Preferences"/> property.
	/// </summary>
	[Column(TypeName = Constants.Sql.DataType.XML)]
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
		set => Preferences = value.ToString();
	}
}
