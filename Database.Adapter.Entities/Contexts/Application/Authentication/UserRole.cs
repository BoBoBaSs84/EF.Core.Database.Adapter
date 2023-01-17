using Microsoft.AspNetCore.Identity;

namespace Database.Adapter.Entities.Contexts.Application.Authentication;

/// <inheritdoc/>
public class UserRole : IdentityUserRole<int>
{
	/// <summary>The <see cref="User"/> property.</summary>
	public virtual User User { get; set; } = default!;
	/// <summary>The <see cref="Role"/> property.</summary>
	public virtual Role Role { get; set; } = default!;
}
