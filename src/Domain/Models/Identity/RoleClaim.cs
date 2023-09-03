using Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public class RoleClaim : IdentityRoleClaim<Guid>
{
	/// <summary>
	/// The <see cref="Role"/> property.
	/// </summary>
	public virtual Role Role { get; set; } = default!;
}
