using Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The interface for the user service.
/// </summary>
public interface IUserService
{
	/// <inheritdoc cref="UserManager{TUser}.AddToRoleAsync(TUser, string)"/>
	Task<IdentityResult> AddToRoleAsync(UserEntity user, string role);
	/// <inheritdoc cref="UserManager{TUser}.CheckPasswordAsync(TUser, string)"/>
	Task<bool> CheckPasswordAsync(UserEntity user, string password);
	/// <inheritdoc cref="UserManager{TUser}.CreateAsync(TUser)"/>
	Task<IdentityResult> CreateAsync(UserEntity user, string password);
	/// <inheritdoc cref="UserManager{TUser}.FindByIdAsync(string)"/>
	Task<UserEntity?> FindByIdAsync(string userId);
	/// <inheritdoc cref="UserManager{TUser}.FindByNameAsync(string)"/>
	Task<UserEntity?> FindByNameAsync(string userName);
	/// <inheritdoc cref="UserManager{TUser}.GetRolesAsync(TUser)"/>
	Task<IList<string>> GetRolesAsync(UserEntity user);
	/// <inheritdoc cref="UserManager{TUser}.GetUsersInRoleAsync(string)"/>
	Task<IList<UserEntity>> GetUsersInRoleAsync(string roleName);
	/// <inheritdoc cref="UserManager{TUser}.RemoveFromRoleAsync(TUser, string)"/>
	Task<IdentityResult> RemoveFromRoleAsync(UserEntity user, string role);
	/// <inheritdoc cref="UserManager{TUser}.UpdateAsync(TUser)"/>
	Task<IdentityResult> UpdateAsync(UserEntity user);
	/// <inheritdoc cref="UserManager{TUser}.GetAuthenticationTokenAsync(TUser, string, string)"/>
	Task<string?> GetAuthenticationTokenAsync(UserEntity user, string loginProvider, string tokenName);
	/// <inheritdoc cref="UserManager{TUser}.GenerateUserTokenAsync(TUser, string, string)"/>
	Task<string> GenerateUserTokenAsync(UserEntity user, string tokenProvider, string purpose);
	/// <inheritdoc cref="UserManager{TUser}.SetAuthenticationTokenAsync(TUser, string, string, string?)"/>
	Task<IdentityResult> SetAuthenticationTokenAsync(UserEntity user, string loginProvider, string tokenName, string? tokenValue);
	/// <inheritdoc cref="UserManager{TUser}.RemoveAuthenticationTokenAsync(TUser, string, string)"/>
	Task<IdentityResult> RemoveAuthenticationTokenAsync(UserEntity user, string loginProvider, string tokenName);
	/// <inheritdoc cref="UserManager{TUser}.VerifyUserTokenAsync(TUser, string, string, string)"/>
	Task<bool> VerifyUserTokenAsync(UserEntity user, string tokenProvider, string purpose, string token);
}
