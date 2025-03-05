using BB84.Home.Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace BB84.Home.Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The interface for the role service.
/// </summary>
public interface IRoleService
{
	/// <inheritdoc cref="RoleManager{TRole}.FindByIdAsync(string)"/>
	Task<RoleEntity?> FindByIdAsync(string roleId);
}
