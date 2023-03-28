using Domain.Common.EntityBaseTypes;

namespace Domain.Entities.Finance;

/// <summary>
/// The account transaction entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedCompositeModel"/> class.
/// </remarks>
public partial class CardTransaction : AuditedCompositeModel
{
	/// <summary>
	/// The <see cref="CardId"/> property.
	/// </summary>
	public int CardId { get; set; } = default!;

	/// <summary>
	/// The <see cref="TransactionId"/> property.
	/// </summary>
	public int TransactionId { get; set; } = default!;
}
