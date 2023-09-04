using Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public class UserLoginModel : IdentityUserLogin<Guid>
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserModel User { get; set; } = default!;
}
