using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using DC = Domain.Constants.DomainConstants;

namespace Domain.Entities.Identity;

/// <inheritdoc/>
public partial class Role : IdentityRole<Guid>
{
	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	[MaxLength(DC.Sql.MaxLength.MAX_500)]
	public string? Description { get; set; } = default!;
}