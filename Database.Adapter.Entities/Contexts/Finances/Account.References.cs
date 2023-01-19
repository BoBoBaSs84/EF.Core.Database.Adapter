namespace Database.Adapter.Entities.Contexts.Finances;

public partial class Account
{
	/// <summary>
	/// The <see cref="AccountUsers"/> property.
	/// </summary>
	public virtual ICollection<AccountUser> AccountUsers { get; set; } = default!;
	/// <summary>
	/// The <see cref="AccountTransactions"/> property.
	/// </summary>
	public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = default!;
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<Card> Cards { get; set; } = default!;
}