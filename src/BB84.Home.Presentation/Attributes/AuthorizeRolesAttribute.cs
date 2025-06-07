using BB84.Home.Domain.Enumerators;
using BB84.Home.Domain.Extensions;

using Microsoft.AspNetCore.Authorization;

namespace BB84.Home.Presentation.Attributes;

/// <summary>
/// Specifies that access to a controller or action is restricted to users in the specified roles.
/// </summary>
/// <remarks>
/// This attribute is used to enforce role-based authorization in ASP.NET applications.
/// It can be applied to controllers or actions to restrict access based on user roles.
/// </remarks>
public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AuthorizeRolesAttribute"/> class with the specified roles.
	/// </summary>
	/// <param name="roles">The roles that are authorized to access a resource or perform an action.</param>
	public AuthorizeRolesAttribute(params RoleType[] roles)
		=> Roles = string.Join(", ", roles.Select(x => x.GetName()));

	/// <summary>
	/// Initializes a new instance of the <see cref="AuthorizeRolesAttribute"/> class with the specified roles.
	/// </summary>
	/// <param name="roles">The roles that are authorized to access a resource or perform an action.</param>
	public AuthorizeRolesAttribute(params string[] roles)
		=> Roles = string.Join(", ", roles);
}
