namespace Database.Adapter.Entities.Contexts.Finances;

public partial class Transaction
{
	/// <summary>The <see cref="Accounts"/> property.</summary>
	public ICollection<Account> Accounts { get; set; } = default!;

	/// <summary>The <see cref="Cards"/> property.</summary>
	public ICollection<Card> Cards { get; set; } = default!;
}
