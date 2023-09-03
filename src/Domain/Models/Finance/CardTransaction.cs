using Domain.Models.Base;

namespace Domain.Models.Finance;

/// <summary>
/// The account transaction entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="CompositeModel"/> class.
/// </remarks>
public partial class CardTransaction : CompositeModel
{
	/// <summary>
	/// The <see cref="CardId"/> property.
	/// </summary>
	public Guid CardId { get; set; }

	/// <summary>
	/// The <see cref="TransactionId"/> property.
	/// </summary>
	public Guid TransactionId { get; set; }
}
