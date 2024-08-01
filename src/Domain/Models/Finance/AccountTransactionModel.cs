using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Finance;

/// <summary>
/// The account transaction model class.
/// </summary>
public sealed class AccountTransactionModel : AuditedCompositeModel
{
	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public Guid AccountId { get; set; }

	/// <summary>
	/// The <see cref="TransactionId"/> property.
	/// </summary>
	public Guid TransactionId { get; set; }

	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public AccountModel Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public TransactionModel Transaction { get; set; } = default!;
}
