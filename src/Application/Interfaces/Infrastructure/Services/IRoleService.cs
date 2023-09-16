using System.Security.Claims;

using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;

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
	Task<IdentityResult> AddClaimAsync(RoleModel role, Claim claim);
	Task<IdentityResult> CreateAsync(RoleModel role);
	Task<IdentityResult> DeleteAsync(RoleModel role);
	Task<RoleModel> FindByIdAsync(string roleId);
	Task<RoleModel> FindByNameAsync(string roleName);
	Task<IList<Claim>> GetClaimsAsync(RoleModel role);
	Task<string> GetRoleIdAsync(RoleModel role);
	Task<string> GetRoleNameAsync(RoleModel role);
	string NormalizeKey(string key);
	Task<IdentityResult> RemoveClaimAsync(RoleModel role, Claim claim);
	Task<bool> RoleExistsAsync(string roleName);
	Task<IdentityResult> SetRoleNameAsync(RoleModel role, string name);
	Task<IdentityResult> UpdateAsync(RoleModel role);
	Task UpdateNormalizedRoleNameAsync(RoleModel role);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
