using Domain.Models.Base;

namespace Domain.Models.Finance;

/// <summary>
/// The account user entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="CompositeModel"/> class.
/// </remarks>
public partial class AccountUser : CompositeModel
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
