using BB84.EntityFrameworkCore.Models;

namespace Domain.Models.Finance;

/// <summary>
/// The account transaction model class.
/// </summary>
public sealed class CardTransactionModel : AuditedCompositeModel
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
	public CardModel Card { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public TransactionModel Transaction { get; set; } = default!;
}
