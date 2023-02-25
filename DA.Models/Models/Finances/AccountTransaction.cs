namespace DA.Domain.Models.Finances;

/// <summary>
/// The account transaction entity class.
/// </summary>
public partial class AccountTransaction
{
	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public int AccountId { get; set; } = default!;
	/// <summary>
	/// The <see cref="TransactionId"/> property.
	/// </summary>
	public int TransactionId { get; set; } = default!;
}
