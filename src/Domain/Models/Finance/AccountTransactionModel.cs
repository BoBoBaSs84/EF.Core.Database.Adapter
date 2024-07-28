using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Finance;

/// <summary>
/// The account transaction model class.
/// </summary>
public partial class AccountTransactionModel : AuditedCompositeModel
{
	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public Guid AccountId { get; set; }

	/// <summary>
	/// The <see cref="TransactionId"/> property.
	/// </summary>
	public Guid TransactionId { get; set; }
}
