using BB84.EntityFrameworkCore.Entities;

namespace BB84.Home.Domain.Entities.Finance;

/// <summary>
/// The account transaction entity class.
/// </summary>
public sealed class CardTransactionEntity : AuditedCompositeEntity
{
	/// <summary>
	/// The <see cref="CardId"/> property.
	/// </summary>
	public Guid CardId { get; set; }

	/// <summary>
	/// The <see cref="TransactionId"/> property.
	/// </summary>
	public Guid TransactionId { get; set; }

	/// <summary>
	/// The <see cref="Card"/> property.
	/// </summary>
	public CardEntity Card { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public TransactionEntity Transaction { get; set; } = default!;
}
