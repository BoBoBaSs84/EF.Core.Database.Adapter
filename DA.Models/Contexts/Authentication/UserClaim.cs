﻿using Microsoft.AspNetCore.Identity;

namespace DA.Models.Contexts.Authentication;

/// <inheritdoc/>
public class UserClaim : IdentityUserClaim<int>
{
	/// <summary>The <see cref="User"/> property.</summary>
	public virtual User User { get; set; } = default!;
}
