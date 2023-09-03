namespace Domain.Models.Finance;

public partial class CardTransaction
{
	/// <summary>
	/// The <see cref="Card"/> property.
	/// </summary>
	public virtual Card Card { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public virtual Transaction Transaction { get; set; } = default!;
}
