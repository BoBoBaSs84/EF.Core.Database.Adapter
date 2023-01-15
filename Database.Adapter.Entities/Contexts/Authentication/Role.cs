using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.Contexts.Authentication;

/// <inheritdoc/>
public class Role : IdentityRole<int>
{
	/// <summary>The <see cref="Description"/> property.</summary>
	[MaxLength(SqlStringLength.MAX_LENGHT_256)]
	public string? Description { get; set; } = default!;

	/// <summary>The <see cref="UserRoles"/> property.</summary>
	public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
	/// <summary>The <see cref="RoleClaims"/> property.</summary>
	public virtual ICollection<RoleClaim> RoleClaims { get; set; } = default!;
}