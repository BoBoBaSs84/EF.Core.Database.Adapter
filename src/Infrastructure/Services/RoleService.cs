using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

/// <summary>
/// The role service class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="RoleManager{TRole}"/> class
/// </remarks>
/// <remarks>
/// Initializes a new instance of the role service class.
/// </remarks>
/// <inheritdoc/>
[ExcludeFromCodeCoverage(Justification = "Wrapper class.")]
internal sealed class RoleService(IRoleStore<RoleModel> store, IEnumerable<IRoleValidator<RoleModel>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<RoleModel>> logger) : RoleManager<RoleModel>(store, roleValidators, keyNormalizer, errors, logger)
{ }
