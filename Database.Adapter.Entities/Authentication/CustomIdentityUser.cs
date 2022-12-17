using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.Authentication;

/// <summary>
/// The <see cref="CustomIdentityUser"/> class, inherits from <see cref="IdentityUser{TKey}"/>
/// </summary>
public class CustomIdentityUser : IdentityUser<Guid>
{
	/// <summary>The <see cref="FirstName"/> property.</summary>
	[MaxLength(SqlStringLength.MAX_LENGHT_64)]
	public string FirstName { get; set; } = default!;
	/// <summary>The <see cref="MiddleName"/> property.</summary>
	[MaxLength(SqlStringLength.MAX_LENGHT_64)]
	public string? MiddleName { get; set; } = default!;
	/// <summary>The <see cref="LastName"/> property.</summary>
	[MaxLength(SqlStringLength.MAX_LENGHT_64)]
	public string LastName { get; set; } = default!;
	/// <summary>The <see cref="DateOfBirth"/> property.</summary>
	[Column(TypeName = SqlDataType.DATE)]
	public DateTime? DateOfBirth { get; set; } = default!;
	/// <summary>The <see cref="Preferences"/> property.</summary>	
	[Column(TypeName = SqlDataType.XML)]
	public string? Preferences { get; set; } = default!;
	/// <summary>The <see cref="XmlPreferencesWrapper"/> property.</summary>
	[NotMapped]
	public XElement XmlPreferencesWrapper
	{
		get => (Preferences is not null) ? XElement.Parse(Preferences) : default!;
		set => Preferences = value.ToString();
	}
}
