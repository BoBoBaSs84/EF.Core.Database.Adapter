using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The role service interface.
/// </summary>
/// <remarks>
/// The interface was generated from the <see cref="RoleManager{TRole}"/> class.
/// </remarks>
public interface IRoleService
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	Task<IdentityResult> AddClaimAsync(Role role, Claim claim);
	Task<IdentityResult> CreateAsync(Role role);
	Task<IdentityResult> DeleteAsync(Role role);
	Task<Role> FindByIdAsync(string roleId);
	Task<Role> FindByNameAsync(string roleName);
	Task<IList<Claim>> GetClaimsAsync(Role role);
	Task<string> GetRoleIdAsync(Role role);
	Task<string> GetRoleNameAsync(Role role);
	string NormalizeKey(string key);
	Task<IdentityResult> RemoveClaimAsync(Role role, Claim claim);
	Task<bool> RoleExistsAsync(string roleName);
	Task<IdentityResult> SetRoleNameAsync(Role role, string name);
	Task<IdentityResult> UpdateAsync(Role role);
	Task UpdateNormalizedRoleNameAsync(Role role);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
