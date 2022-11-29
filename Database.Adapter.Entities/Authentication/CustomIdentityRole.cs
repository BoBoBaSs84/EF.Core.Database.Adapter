using Microsoft.AspNetCore.Identity;

namespace Database.Adapter.Entities.Authentication;

/// <summary>
/// The <see cref="CustomIdentityRole"/> class inherits from <see cref="IdentityRole{TKey}"/>
/// </summary>
public class CustomIdentityRole : IdentityRole<Guid>
{
	/// <summary>The <see cref="Description"/> property.</summary>
	public string Description { get; set; } = default!;
}