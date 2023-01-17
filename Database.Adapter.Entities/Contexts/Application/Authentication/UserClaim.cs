using Microsoft.AspNetCore.Identity;

namespace Database.Adapter.Entities.Contexts.Application.Authentication;

/// <inheritdoc/>
public class UserClaim : IdentityUserClaim<int>
{
	/// <summary>The <see cref="User"/> property.</summary>
	public virtual User User { get; set; } = default!;
}
