namespace Database.Adapter.Entities.Contexts.Finances;

public partial class Transaction
{
	/// <summary>
	/// The <see cref="AccountTransactions"/> property.
	/// </summary>
	public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = default!;
	/// <summary>
	/// The <see cref="CardTransactions"/> property.
	/// </summary>
	public virtual ICollection<CardTransaction> CardTransactions { get; set; } = default!;
}
