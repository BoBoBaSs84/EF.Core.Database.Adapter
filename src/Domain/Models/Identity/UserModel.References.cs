using Domain.Models.Attendance;
using Domain.Models.Finance;

namespace Domain.Models.Identity;

public partial class UserModel
{
	/// <summary>
	/// The <see cref="Claims"/> property.
	/// </summary>
	public virtual ICollection<UserClaimModel> Claims { get; set; } = [];

	/// <summary>
	/// The <see cref="Logins"/> property.
	/// </summary>
	public virtual ICollection<UserLoginModel> Logins { get; set; } = [];

	/// <summary>
	/// The <see cref="Tokens"/> property.
	/// </summary>
	public virtual ICollection<UserTokenModel> Tokens { get; set; } = [];

	/// <summary>
	/// The <see cref="UserRoles"/> property.
	/// </summary>
	public virtual ICollection<UserRoleModel> UserRoles { get; set; } = [];

	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	public virtual ICollection<AttendanceModel> Attendances { get; set; } = [];

	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUserModel> AccountUsers { get; set; } = [];

	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<CardModel> Cards { get; set; } = [];
}
