using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Finance;

/// <summary>
/// The account user model class.
/// </summary>
public partial class AccountUserModel : AuditedCompositeModel
{
	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public Guid AccountId { get; set; }

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public Guid UserId { get; set; }
}
