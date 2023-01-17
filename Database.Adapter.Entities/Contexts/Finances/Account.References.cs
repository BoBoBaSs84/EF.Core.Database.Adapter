using Database.Adapter.Entities.Contexts.Application.Authentication;

namespace Database.Adapter.Entities.Contexts.Finances;

public partial class Account
{
	/// <summary>The <see cref="Users"/> property.</summary>
	public virtual ICollection<User> Users { get; set; } = default!;
	/// <summary>The <see cref="Transactions"/> property.</summary>
	public virtual ICollection<Transaction> Transactions { get; set; } = default!;
	/// <summary>The <see cref="Cards"/> property.</summary>
	public virtual ICollection<Card> Cards { get; set; } = default!;
}