namespace Database.Adapter.Entities.Contexts.Finances;

public partial class AccountTransaction
{
	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public virtual Account Account { get; set; } = default!;
	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public virtual Transaction Transaction { get; set; } = default!;
}
