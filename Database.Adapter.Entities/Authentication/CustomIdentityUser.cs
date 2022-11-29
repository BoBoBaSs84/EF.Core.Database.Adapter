using Microsoft.AspNetCore.Identity;

namespace Database.Adapter.Entities.Authentication;

/// <summary>
/// The <see cref="CustomIdentityUser"/> class, inherits from <see cref="IdentityUser{TKey}"/>
/// </summary>
public class CustomIdentityUser : IdentityUser<Guid>
{
	/// <summary>The <see cref="FirstName"/> property.</summary>
	public string FirstName { get; set; } = default!;

	/// <summary>The <see cref="MiddleName"/> property.</summary>
	public string? MiddleName { get; set; } = default!;

	/// <summary>The <see cref="LastName"/> property.</summary>
	public string LastName { get; set; } = default!;

	/// <summary>The <see cref="DateOfBirth"/> property.</summary>
	public DateTime? DateOfBirth { get; set; } = default!;

	/// <summary>The <see cref="Preferences"/> property.</summary>
	public string Preferences { get; set; } = default!;
}
