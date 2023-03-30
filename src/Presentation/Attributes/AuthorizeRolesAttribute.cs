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
/// Serves as wrapper attribute class to enable role authorization via <see cref="RoleTypes"/> enumerators.
/// </remarks>
public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
	/// <summary>
	/// Initializes an instance of <see cref="AuthorizeRolesAttribute"/>.
	/// </summary>
	/// <param name="roles">The roles to authorize.</param>
	public AuthorizeRolesAttribute(params RoleTypes[] roles)
		=> Roles = string.Join(", ", roles.Select(x => x.GetName()));
}
