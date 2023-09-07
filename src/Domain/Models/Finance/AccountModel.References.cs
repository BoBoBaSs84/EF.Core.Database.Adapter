namespace Domain.Models.Finance;

public partial class AccountModel
{
	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUserModel> AccountUsers { get; set; } = new HashSet<AccountUserModel>();

	/// <summary>
	/// The <see cref="AccountTransactions"/> property.
	/// </summary>
	public virtual ICollection<AccountTransactionModel> AccountTransactions { get; set; } = new HashSet<AccountTransactionModel>();

	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<CardModel> Cards { get; set; } = new HashSet<CardModel>();
}