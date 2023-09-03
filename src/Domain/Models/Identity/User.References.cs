﻿using Domain.Models.Finance;
using Domain.Models.Attendance;
using Domain.Models.Identity;

namespace Domain.Entities.Identity;

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
	public virtual ICollection<AttendanceModel> Attendances { get; set; } = new HashSet<AttendanceModel>();

	/// <summary>
	/// The <see cref="AttendanceSettings"/> property.
	/// </summary>
	public virtual AttendanceSettingsModel AttendanceSettings { get; set; } = default!;

	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUser> AccountUsers { get; set; } = new HashSet<AccountUser>();

	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
}
