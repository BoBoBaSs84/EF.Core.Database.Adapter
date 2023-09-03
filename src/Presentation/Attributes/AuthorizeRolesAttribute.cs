using Domain.Enumerators;
using Domain.Extensions;

using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes;

/// <summary>
/// The authorize roles attribute class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuthorizeAttribute"/> class.
/// <br></br>
/// Serves as wrapper attribute class to enable role authorization via <see cref="RoleType"/> enumerators.
/// </remarks>
public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
	/// <summary>
	/// Initializes an instance of the authorize roles attribute class.
	/// </summary>
	/// <param name="roles">The roles to authorize.</param>
	public AuthorizeRolesAttribute(params RoleType[] roles)
		=> Roles = string.Join(", ", roles.Select(x => x.GetName()));

	/// <summary>
	/// Initializes an instance of the authorize roles attribute class.
	/// </summary>
	/// <param name="roles">The roles to authorize.</param>
	public AuthorizeRolesAttribute(params string[] roles)
		=> Roles = string.Join(", ", roles);
}
