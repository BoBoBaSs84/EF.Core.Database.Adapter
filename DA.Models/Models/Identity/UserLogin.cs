using Microsoft.AspNetCore.Identity;

namespace DA.Domain.Models.Identity;

/// <inheritdoc/>
public class UserLogin : IdentityUserLogin<int>
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual User User { get; set; } = default!;
}
