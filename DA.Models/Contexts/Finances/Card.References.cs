using DA.Models.Contexts.Authentication;
using DA.Models.Contexts.MasterData;

namespace DA.Models.Contexts.Finances;

public partial class Card
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual User User { get; set; } = default!;
	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public virtual Account Account { get; set; } = default!;
	/// <summary>
	/// The <see cref="CardTransactions"/> property.
	/// </summary>
	public virtual ICollection<CardTransaction> CardTransactions { get; set; } = default!;
	/// <summary>
	/// The <see cref="CardType"/> property.
	/// </summary>
	public virtual CardType CardType { get; set; } = default!;
}
