using DA.Models.Contexts.Finances;

namespace DA.Models.Contexts.MasterData;

public partial class CardType
{
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
}
