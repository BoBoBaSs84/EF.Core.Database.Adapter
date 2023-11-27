using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public class RoleClaimModel : IdentityRoleClaim<Guid>
{
	/// <summary>
	/// The <see cref="Role"/> property.
	/// </summary>
	public virtual RoleModel Role { get; set; } = default!;
}
