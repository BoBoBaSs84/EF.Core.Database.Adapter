using Domain.Entities.Finance;

namespace Domain.Entities.Enumerator;

public partial class CardType
{
	/// <summary>
	/// The <see cref="Cards"/> property.
	/// </summary>
	public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
}
