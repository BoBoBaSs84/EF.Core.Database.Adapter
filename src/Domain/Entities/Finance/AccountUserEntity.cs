using BB84.EntityFrameworkCore.Entities;

using Domain.Entities.Identity;

namespace Domain.Entities.Finance;

/// <summary>
/// The account user entity class.
/// </summary>
public sealed class AccountUserEntity : AuditedCompositeEntity
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
	public AccountEntity Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public UserEntity User { get; set; } = default!;
}
