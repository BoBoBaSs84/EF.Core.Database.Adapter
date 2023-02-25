using DA.Domain.Models.Identity;

namespace DA.Domain.Models.Finances;

public partial class AccountUser
{
	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public virtual Account Account { get; set; } = default!;
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual User User { get; set; } = default!;
}
