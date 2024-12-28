using Domain.Entities.Identity;

namespace Domain.Models.Identity;

public partial class RoleModel
{
	/// <summary>
	/// The <see cref="UserRoles"/> property.
	/// </summary>
	public virtual ICollection<UserRoleModel> UserRoles { get; set; } = [];

	/// <summary>
	/// The <see cref="RoleClaims"/> property.
	/// </summary>
	public virtual ICollection<RoleClaimModel> RoleClaims { get; set; } = [];
}
