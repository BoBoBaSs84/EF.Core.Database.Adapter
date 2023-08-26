using Domain.Common.EntityBaseTypes;

namespace Domain.Entities.Finance;

/// <summary>
/// The account user entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedCompositeModel"/> class.
/// </remarks>
public partial class AccountUser : AuditedCompositeModel
{
	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public int AccountId { get; set; } = default!;

	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public int UserId { get; set; } = default!;
}
