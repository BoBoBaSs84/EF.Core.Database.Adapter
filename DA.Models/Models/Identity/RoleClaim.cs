using Microsoft.AspNetCore.Identity;

namespace DA.Domain.Models.Identity;

/// <inheritdoc/>
public class RoleClaim : IdentityRoleClaim<int>
{
	/// <summary>
	/// The <see cref="Role"/> property.
	/// </summary>
	public virtual Role Role { get; set; } = default!;
}
