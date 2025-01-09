using Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public partial class UserModel : IdentityUser<Guid>
{
	/// <summary>
	/// The <see cref="FirstName"/> property.
	/// </summary>
	public required string FirstName { get; set; }

	/// <summary>
	/// The <see cref="MiddleName"/> property.
	/// </summary>
	public string? MiddleName { get; set; }

	/// <summary>
	/// The <see cref="LastName"/> property.
	/// </summary>
	public required string LastName { get; set; }

	/// <summary>
	/// The <see cref="DateOfBirth"/> property.
	/// </summary>
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
