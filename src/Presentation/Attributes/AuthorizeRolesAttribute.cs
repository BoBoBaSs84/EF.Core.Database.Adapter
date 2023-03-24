using Domain.Enumerators;
using Domain.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes;

/// <summary>
/// The authorize roles attribute class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuthorizeAttribute"/> class.
/// Serves as wrapper class to enable role authorization via enumerators.
/// </remarks>
public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
	/// <summary>
	/// Initializes an instance of <see cref="AuthorizeRolesAttribute"/>.
	/// </summary>
	/// <param name="roles"></param>
	public AuthorizeRolesAttribute(params RoleTypes[] roles) =>
		Roles = string.Join(", ", roles.Select(x => x.GetName()));
}
