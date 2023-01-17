using Database.Adapter.Entities.Contexts.Application.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Database.Adapter.Entities.Contexts.Authentication;

/// <inheritdoc/>
public class RoleClaim : IdentityRoleClaim<int>
{
	/// <summary>The <see cref="Role"/> property.</summary>
	public virtual Role Role { get; set; } = default!;
}
