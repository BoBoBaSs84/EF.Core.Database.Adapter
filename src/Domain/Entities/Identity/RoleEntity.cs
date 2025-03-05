using Microsoft.AspNetCore.Identity;

namespace BB84.Home.Domain.Entities.Identity;

/// <inheritdoc/>
public sealed class RoleEntity : IdentityRole<Guid>
{
	/// <summary>
	/// The description of the role.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// The navigational property to the <see cref="UserRoleEntity"/>.
	/// </summary>
	public ICollection<UserRoleEntity> UserRoles { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="RoleClaimEntity"/>.
	/// </summary>
	public ICollection<RoleClaimEntity> RoleClaims { get; set; } = [];
}