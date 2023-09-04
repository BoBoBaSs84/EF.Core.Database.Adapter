namespace Domain.Models.Finance;

public partial class CardTransactionModel
{
	/// <summary>
	/// The <see cref="Card"/> property.
	/// </summary>
	public virtual CardModel Card { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public virtual TransactionModel Transaction { get; set; } = default!;
}
