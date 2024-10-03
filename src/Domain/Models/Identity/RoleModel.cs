using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public partial class RoleModel : IdentityRole<Guid>
{
	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	public string? Description { get; set; }
}