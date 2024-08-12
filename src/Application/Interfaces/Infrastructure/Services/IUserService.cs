using Domain.Models.Identity;

using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The interface for the user service.
/// </summary>
public interface IUserService
{
	/// <inheritdoc cref="UserManager{TUser}.AddToRoleAsync(TUser, string)"/>
	Task<IdentityResult> AddToRoleAsync(UserModel user, string role);
	/// <inheritdoc cref="UserManager{TUser}.CheckPasswordAsync(TUser, string)"/>
	Task<bool> CheckPasswordAsync(UserModel user, string password);
	/// <inheritdoc cref="UserManager{TUser}.CreateAsync(TUser)"/>
	Task<IdentityResult> CreateAsync(UserModel user, string password);
	/// <inheritdoc cref="UserManager{TUser}.FindByIdAsync(string)"/>
	Task<UserModel?> FindByIdAsync(string userId);
	/// <inheritdoc cref="UserManager{TUser}.FindByNameAsync(string)"/>
	Task<UserModel?> FindByNameAsync(string userName);
	/// <inheritdoc cref="UserManager{TUser}.GetRolesAsync(TUser)"/>
	Task<IList<string>> GetRolesAsync(UserModel user);
	/// <inheritdoc cref="UserManager{TUser}.GetUsersInRoleAsync(string)"/>
	Task<IList<UserModel>> GetUsersInRoleAsync(string roleName);
	/// <inheritdoc cref="UserManager{TUser}.RemoveFromRoleAsync(TUser, string)"/>
	Task<IdentityResult> RemoveFromRoleAsync(UserModel user, string role);
	/// <inheritdoc cref="UserManager{TUser}.UpdateAsync(TUser)"/>
	Task<IdentityResult> UpdateAsync(UserModel user);
}
