namespace DA.Domain.Models.Identity;

public partial class Role
{
	/// <summary>
	/// The <see cref="UserRoles"/> property.
	/// </summary>
	public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
	/// <summary>
	/// The <see cref="RoleClaims"/> property.
	/// </summary>
	public virtual ICollection<RoleClaim> RoleClaims { get; set; } = new HashSet<RoleClaim>();
}
