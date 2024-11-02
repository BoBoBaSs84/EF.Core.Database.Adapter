using Domain.Models.Attendance;
using Domain.Models.Documents;
using Domain.Models.Finance;
using Domain.Models.Todo;

namespace Domain.Models.Identity;

public partial class UserModel
{
	/// <summary>
	/// The <see cref="Claims"/> property.
	/// </summary>
	public virtual ICollection<UserClaimModel> Claims { get; set; } = default!;

	/// <summary>
	/// The <see cref="Logins"/> property.
	/// </summary>
	public virtual ICollection<UserLoginModel> Logins { get; set; } = default!;

	/// <summary>
	/// The <see cref="Tokens"/> property.
	/// </summary>
	public virtual ICollection<UserTokenModel> Tokens { get; set; } = default!;

	/// <summary>
	/// The <see cref="UserRoles"/> property.
	/// </summary>
	public virtual ICollection<UserRoleModel> UserRoles { get; set; } = default!;

	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	public virtual ICollection<AttendanceModel> Attendances { get; set; } = default!;

	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUserModel> AccountUsers { get; set; } = default!;

	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<CardModel> Cards { get; set; } = default!;

	/// <summary>
	/// The <see cref="TodoLists"/> property.
	/// </summary>
	public virtual ICollection<List> TodoLists { get; set; } = default!;

	/// <summary>
	/// The <see cref="Documents"/> property.
	/// </summary>
	public virtual ICollection<Document> Documents { get; set; } = default!;
}
