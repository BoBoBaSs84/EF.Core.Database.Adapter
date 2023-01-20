using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Database.Adapter.Entities.Constants.Sql;

namespace Database.Adapter.Entities.Contexts.Authentication;

/// <inheritdoc/>
public partial class Role : IdentityRole<int>
{
	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	[MaxLength(StringLength.MAX_LENGHT_256)]
	public string? Description { get; set; } = default!;
}