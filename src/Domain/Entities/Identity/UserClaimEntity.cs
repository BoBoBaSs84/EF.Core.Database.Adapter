using Microsoft.AspNetCore.Identity;

namespace BB84.Home.Domain.Entities.Identity;

/// <inheritdoc/>
public sealed class UserClaimEntity : IdentityUserClaim<Guid>
{
	/// <summary>
	/// The navigational property to the <see cref="UserEntity"/>.
	/// </summary>
	public UserEntity User { get; set; } = default!;
}
