using BB84.EntityFrameworkCore.Entities;

namespace Domain.Entities.Finance;

/// <summary>
/// The account transaction model class.
/// </summary>
public sealed class AccountTransactionEntity : AuditedCompositeEntity
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
	public AccountEntity Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public TransactionEntity Transaction { get; set; } = default!;
}
