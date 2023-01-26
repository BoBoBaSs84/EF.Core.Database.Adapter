using DA.Models.Contexts.Finances;
using DA.Models.Contexts.Timekeeping;

namespace DA.Models.Contexts.Authentication;

public partial class User
{
	/// <summary>
	/// The <see cref="Claims"/> property.
	/// </summary>
	public virtual ICollection<UserClaim> Claims { get; set; } = new HashSet<UserClaim>();
	/// <summary>
	/// The <see cref="Logins"/> property.
	/// </summary>
	public virtual ICollection<UserLogin> Logins { get; set; } = new HashSet<UserLogin>();
	/// <summary>
	/// The <see cref="Tokens"/> property.
	/// </summary>
	public virtual ICollection<UserToken> Tokens { get; set; } = new HashSet<UserToken>();
	/// <summary>
	/// The <see cref="UserRoles"/> property.
	/// </summary>
	public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
	/// <summary>
	/// The <see cref="Attendances"/> property.
	/// </summary>
	public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUser> AccountUsers { get; set; } = new HashSet<AccountUser>();
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
}
