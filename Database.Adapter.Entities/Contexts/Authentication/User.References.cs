using Database.Adapter.Entities.Contexts.Application.Timekeeping;
using Database.Adapter.Entities.Contexts.Authentication;

namespace Database.Adapter.Entities.Contexts.Application.Authentication;
public partial class User
{
	/// <summary>The <see cref="Claims"/> property.</summary>
	public virtual ICollection<UserClaim> Claims { get; set; } = default!;
	/// <summary>The <see cref="Logins"/> property.</summary>
	public virtual ICollection<UserLogin> Logins { get; set; } = default!;
	/// <summary>The <see cref="Tokens"/> property.</summary>
	public virtual ICollection<UserToken> Tokens { get; set; } = default!;
	/// <summary>The <see cref="UserRoles"/> property.</summary>
	public virtual ICollection<UserRole> UserRoles { get; set; } = default!;

	/// <summary>The <see cref="Attendances"/> property.</summary>
	public virtual ICollection<Attendance> Attendances { get; set; } = default!;
}
