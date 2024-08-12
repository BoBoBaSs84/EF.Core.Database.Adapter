using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The interface for the role service.
/// </summary>
public interface IRoleService
{
	/// <inheritdoc cref="RoleManager{TRole}.FindByIdAsync(string)"/>
	Task<RoleModel?> FindByIdAsync(string roleId);
}
