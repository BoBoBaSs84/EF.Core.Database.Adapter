using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static DA.Models.Constants.Sql;

namespace DA.Models.Contexts.Authentication;

/// <inheritdoc/>
public partial class Role : IdentityRole<int>
{
	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_500)]
	public string? Description { get; set; } = default!;
}