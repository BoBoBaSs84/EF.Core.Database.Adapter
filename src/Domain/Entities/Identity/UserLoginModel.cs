using Microsoft.AspNetCore.Identity;

namespace BB84.Home.Domain.Entities.Identity;

/// <inheritdoc/>
public class UserLoginModel : IdentityUserLogin<Guid>
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserEntity User { get; set; } = default!;
}
