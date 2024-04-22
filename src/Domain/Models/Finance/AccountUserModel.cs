using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Finance;

/// <summary>
/// The account user model class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="CompositeModel"/> class.
/// </remarks>
public partial class AccountUserModel : CompositeModel
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
