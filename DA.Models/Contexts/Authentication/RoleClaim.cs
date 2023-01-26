using DA.Models.Contexts.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DA.Models.Contexts.Authentication;

/// <inheritdoc/>
public class RoleClaim : IdentityRoleClaim<int>
{
	/// <summary>
	/// The <see cref="Role"/> property.
	/// </summary>
	public virtual Role Role { get; set; } = default!;
}
