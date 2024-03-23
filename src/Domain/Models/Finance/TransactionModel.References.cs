namespace Domain.Models.Finance;

public partial class TransactionModel
{
	/// <summary>
	/// The <see cref="AccountTransactions"/> property.
	/// </summary>
	public virtual ICollection<AccountTransactionModel> AccountTransactions { get; set; } = [];

	/// <summary>
	/// The <see cref="CardTransactions"/> property.
	/// </summary>
	public virtual ICollection<CardTransactionModel> CardTransactions { get; set; } = [];
}
