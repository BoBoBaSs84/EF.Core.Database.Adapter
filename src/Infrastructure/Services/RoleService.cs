﻿using Application.Interfaces.Infrastructure.Services;

using Domain.Entities.Identity;

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
internal sealed class RoleService(IRoleStore<RoleEntity> store, IEnumerable<IRoleValidator<RoleEntity>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<RoleEntity>> logger) : RoleManager<RoleEntity>(store, roleValidators, keyNormalizer, errors, logger), IRoleService
{ }
