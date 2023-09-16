using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using SqlMaxLength = Domain.Constants.DomainConstants.Sql.MaxLength;

namespace Domain.Models.Identity;

/// <inheritdoc/>
public partial class RoleModel : IdentityRole<Guid>
{
	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	[MaxLength(SqlMaxLength.MAX_500)]
	public string? Description { get; set; }
}