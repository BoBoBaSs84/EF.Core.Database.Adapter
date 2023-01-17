using Database.Adapter.Entities.Contexts.Finances;

namespace Database.Adapter.Entities.Contexts.MasterData;

public partial class CardType
{
	/// <summary>The <see cref="Cards"/> property.</summary>
	public virtual ICollection<Card> Cards { get; set; } = default!;
}
