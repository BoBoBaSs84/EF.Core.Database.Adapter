using DA.Domain.Models.Identity;
using DA.Infrastructure.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DA.Infrastructure.Application;

/// <summary>
/// The role service class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="RoleManager{TRole}"/> class.
/// </remarks>
internal sealed class RoleService : RoleManager<Role>, IRoleService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RoleService"/> class.
	/// </summary>
	/// <inheritdoc/>
	public RoleService(
		IRoleStore<Role> store,
		IEnumerable<IRoleValidator<Role>> roleValidators,
		ILookupNormalizer keyNormalizer,
		IdentityErrorDescriber errors,
		ILogger<RoleManager<Role>> logger)
		: base(store, roleValidators, keyNormalizer, errors, logger)
	{
	}
}
