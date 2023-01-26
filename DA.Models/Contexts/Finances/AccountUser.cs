namespace DA.Models.Contexts.Finances;

/// <summary>
/// The account user entity class.
/// </summary>
public partial class AccountUser
{
	/// <summary>
	/// The <see cref="AccountId"/> property.
	/// </summary>
	public int AccountId { get; set; } = default!;
	/// <summary>
	/// The <see cref="UserId"/> property.
	/// </summary>
	public int UserId { get; set; } = default!;
}
