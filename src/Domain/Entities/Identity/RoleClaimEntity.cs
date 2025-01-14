﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

/// <inheritdoc/>
public sealed class RoleClaimEntity : IdentityRoleClaim<Guid>
{
	/// <summary>
	/// The navigational property to the <see cref="RoleEntity"/>.
	/// </summary>
	public RoleEntity Role { get; set; } = default!;
}