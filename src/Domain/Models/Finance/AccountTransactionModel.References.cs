namespace Domain.Models.Finance;

public partial class AccountTransactionModel
{
	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public virtual AccountModel Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="Transaction"/> property.
	/// </summary>
	public virtual TransactionModel Transaction { get; set; } = default!;
}
