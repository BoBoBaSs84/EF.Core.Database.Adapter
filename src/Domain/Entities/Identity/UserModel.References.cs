using Domain.Entities.Attendance;
using Domain.Entities.Documents;
using Domain.Entities.Finance;
using Domain.Entities.Identity;
using Domain.Entities.Todo;

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
	public virtual ICollection<AttendanceEntity> Attendances { get; set; } = default!;

	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUserEntity> AccountUsers { get; set; } = default!;

	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<CardEntity> Cards { get; set; } = default!;

	/// <summary>
	/// The <see cref="TodoLists"/> property.
	/// </summary>
	public virtual ICollection<ListEntity> TodoLists { get; set; } = default!;

	/// <summary>
	/// The <see cref="Documents"/> property.
	/// </summary>
	public virtual ICollection<DocumentEntity> Documents { get; set; } = default!;
}
