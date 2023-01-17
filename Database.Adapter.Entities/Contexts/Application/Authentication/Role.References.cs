namespace Database.Adapter.Entities.Contexts.Application.Authentication;

public partial class Role
{
	/// <summary>The <see cref="UserRoles"/> property.</summary>
	public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
	/// <summary>The <see cref="RoleClaims"/> property.</summary>
	public virtual ICollection<RoleClaim> RoleClaims { get; set; } = default!;
}
