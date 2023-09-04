namespace Domain.Models.Finance;

public partial class TransactionModel
{
	/// <summary>
	/// The <see cref="AccountTransactions"/> property.
	/// </summary>
	public virtual ICollection<AccountTransactionModel> AccountTransactions { get; set; } = new HashSet<AccountTransactionModel>();

	/// <summary>
	/// The <see cref="CardTransactions"/> property.
	/// </summary>
	public virtual ICollection<CardTransactionModel> CardTransactions { get; set; } = new HashSet<CardTransactionModel>();
}
