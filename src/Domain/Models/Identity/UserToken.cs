﻿using Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public class UserToken : IdentityUserToken<Guid>
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual User User { get; set; } = default!;
}
