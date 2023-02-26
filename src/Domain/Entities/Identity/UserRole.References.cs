namespace Domain.Entities.Identity;

public partial class UserRole
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual User User { get; set; } = default!;
	/// <summary>
	/// The <see cref="Role"/> property.
	/// </summary>
	public virtual Role Role { get; set; } = default!;
}
