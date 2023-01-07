using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Constants.SqlConstants;

namespace Database.Adapter.Entities.Authentication;

/// <summary>
/// The <see cref="CustomIdentityRole"/> class inherits from <see cref="IdentityRole{TKey}"/>
/// </summary>
public class CustomIdentityRole : IdentityRole<int>
{
	/// <summary>The <see cref="Description"/> property.</summary>
	[MaxLength(SqlStringLength.MAX_LENGHT_256)]
	public string? Description { get; set; } = default!;
}