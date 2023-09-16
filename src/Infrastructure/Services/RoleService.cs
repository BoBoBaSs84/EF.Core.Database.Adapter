using Application.Interfaces.Infrastructure.Services;

using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

/// <summary>
/// The role service class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="RoleManager{TRole}"/> class
/// and implements the members of the <see cref="IRoleService"/>.
/// </remarks>
internal sealed class RoleService : RoleManager<RoleModel>, IRoleService
{
	/// <summary>
	/// Initializes a new instance of the role service class.
	/// </summary>
	/// <inheritdoc/>
	public RoleService(IRoleStore<RoleModel> store, IEnumerable<IRoleValidator<RoleModel>> roleValidators, ILookupNormalizer keyNormalizer,
		IdentityErrorDescriber errors, ILogger<RoleManager<RoleModel>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
	{ }
}
