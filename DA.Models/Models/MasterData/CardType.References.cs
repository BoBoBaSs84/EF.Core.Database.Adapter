using DA.Domain.Models.Finances;

namespace DA.Domain.Models.MasterData;

public partial class CardType
{
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
}
