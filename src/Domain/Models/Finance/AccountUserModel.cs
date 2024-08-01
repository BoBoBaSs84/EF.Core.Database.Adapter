using BB84.EntityFrameworkCore.Models;

using Domain.Models.Identity;

namespace Domain.Models.Finance;

/// <summary>
/// The account user model class.
/// </summary>
public sealed class AccountUserModel : AuditedCompositeModel
{
	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public Guid AccountId { get; set; }

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public AccountModel Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public UserModel User { get; set; } = default!;
}
