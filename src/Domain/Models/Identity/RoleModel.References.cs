using Domain.Models.Identity;

namespace Domain.Entities.Identity;

public partial class RoleModel
{
	/// <summary>
	/// The <see cref="UserRoles"/> property.
	/// </summary>
	public virtual ICollection<UserRoleModel> UserRoles { get; set; } = new HashSet<UserRoleModel>();

	/// <summary>
	/// The <see cref="RoleClaims"/> property.
	/// </summary>
	public virtual ICollection<RoleClaimModel> RoleClaims { get; set; } = new HashSet<RoleClaimModel>();
}
