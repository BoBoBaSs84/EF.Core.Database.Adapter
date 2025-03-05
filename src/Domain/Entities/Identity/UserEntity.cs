using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Entities.Todo;

using Microsoft.AspNetCore.Identity;

namespace BB84.Home.Domain.Entities.Identity;

/// <inheritdoc/>
public sealed class UserEntity : IdentityUser<Guid>
{
	/// <summary>
	/// The first name of the user.
	/// </summary>
	public required string FirstName { get; set; }

	/// <summary>
	/// The middle name of the user.
	/// </summary>
	public string? MiddleName { get; set; }

	/// <summary>
	/// The last name of the user.
	/// </summary>
	public required string LastName { get; set; }

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	public DateTime? DateOfBirth { get; set; }

	/// <summary>
	/// The preferences of the user.
	/// </summary>
	public PreferencesModel? Preferences { get; set; }

	/// <summary>
	/// The picture of the user.
	/// </summary>
	public byte[]? Picture { get; set; }

	/// <summary>
	/// The navigational property to the <see cref="UserClaimEntity"/>.
	/// </summary>
	public ICollection<UserClaimEntity> Claims { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="UserLoginModel"/>.
	/// </summary>
	public ICollection<UserLoginModel> Logins { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="UserTokenModel"/>.
	/// </summary>
	public ICollection<UserTokenModel> Tokens { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="UserRoleEntity"/>.
	/// </summary>
	public ICollection<UserRoleEntity> UserRoles { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="AttendanceEntity"/>.
	/// </summary>
	public ICollection<AttendanceEntity> Attendances { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="AccountUserEntity"/>.
	/// </summary>
	public ICollection<AccountUserEntity> AccountUsers { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="CardEntity"/>.
	/// </summary>
	public ICollection<CardEntity> Cards { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="ListEntity"/>.
	/// </summary>
	public ICollection<ListEntity> TodoLists { get; set; } = [];

	/// <summary>
	/// The navigational property to the <see cref="DocumentEntity"/>.
	/// </summary>
	public ICollection<DocumentEntity> Documents { get; set; } = [];
}
