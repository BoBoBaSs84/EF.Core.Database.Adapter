namespace Domain.Entities.Identity;

public partial class UserRoleModel
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserModel User { get; set; } = default!;
	/// <summary>
	/// The <see cref="Role"/> property.
	/// </summary>
	public virtual RoleModel Role { get; set; } = default!;
}
