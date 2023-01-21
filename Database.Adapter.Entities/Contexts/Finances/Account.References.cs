namespace Database.Adapter.Entities.Contexts.Finances;

public partial class Account
{
	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUser> AccountUsers { get; set; } = new HashSet<AccountUser>();
	/// <summary>
	/// The <see cref="AccountTransactions"/> property.
	/// </summary>
	public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new HashSet<AccountTransaction>();
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
}