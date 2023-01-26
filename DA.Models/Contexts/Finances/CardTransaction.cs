namespace DA.Models.Contexts.Finances;

/// <summary>
/// The account transaction entity class.
/// </summary>
public partial class CardTransaction
{
	/// <summary>
	/// The <see cref="CardId"/> property.
	/// </summary>
	public int CardId { get; set; } = default!;

	/// <summary>
	/// The <see cref="TransactionId"/> property.
	/// </summary>
	public int TransactionId { get; set; } = default!;
}
