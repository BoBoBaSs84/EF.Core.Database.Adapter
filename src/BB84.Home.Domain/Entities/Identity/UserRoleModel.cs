using Microsoft.AspNetCore.Identity;

namespace BB84.Home.Domain.Entities.Identity;

/// <inheritdoc/>
public sealed class UserRoleEntity : IdentityUserRole<Guid>
{
	/// <summary>
	/// The navigational property to the <see cref="UserEntity"/>.
	/// </summary>
	public UserEntity User { get; set; } = default!;

	/// <summary>
	/// The navigational property to the <see cref="RoleEntity"/>.
	/// </summary>
	public RoleEntity Role { get; set; } = default!;
}
