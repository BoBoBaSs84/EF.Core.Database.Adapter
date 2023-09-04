using System.Security.Claims;

using Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The user service interface.
/// </summary>
/// <remarks>
/// The interface was generated from the <see cref="UserManager{User}"/> class.
/// </remarks>
public interface IUserService
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	Task<IdentityResult> AccessFailedAsync(UserModel user);
	Task<IdentityResult> AddClaimAsync(UserModel user, Claim claim);
	Task<IdentityResult> AddClaimsAsync(UserModel user, IEnumerable<Claim> claims);
	Task<IdentityResult> AddLoginAsync(UserModel user, UserLoginInfo login);
	Task<IdentityResult> AddPasswordAsync(UserModel user, string password);
	Task<IdentityResult> AddToRoleAsync(UserModel user, string role);
	Task<IdentityResult> AddToRolesAsync(UserModel user, IEnumerable<string> roles);
	Task<IdentityResult> ChangeEmailAsync(UserModel user, string newEmail, string token);
	Task<IdentityResult> ChangePasswordAsync(UserModel user, string currentPassword, string newPassword);
	Task<IdentityResult> ChangePhoneNumberAsync(UserModel user, string phoneNumber, string token);
	Task<bool> CheckPasswordAsync(UserModel user, string password);
	Task<IdentityResult> ConfirmEmailAsync(UserModel user, string token);
	Task<int> CountRecoveryCodesAsync(UserModel user);
	Task<IdentityResult> CreateAsync(UserModel user);
	Task<IdentityResult> CreateAsync(UserModel user, string password);
	Task<byte[]> CreateSecurityTokenAsync(UserModel user);
	Task<IdentityResult> DeleteAsync(UserModel user);
	Task<UserModel> FindByEmailAsync(string email);
	Task<UserModel> FindByIdAsync(string userId);
	Task<UserModel> FindByLoginAsync(string loginProvider, string providerKey);
	Task<UserModel> FindByNameAsync(string userName);
	Task<string> GenerateChangeEmailTokenAsync(UserModel user, string newEmail);
	Task<string> GenerateChangePhoneNumberTokenAsync(UserModel user, string phoneNumber);
	Task<string> GenerateConcurrencyStampAsync(UserModel user);
	Task<string> GenerateEmailConfirmationTokenAsync(UserModel user);
	string GenerateNewAuthenticatorKey();
	Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(UserModel user, int number);
	Task<string> GeneratePasswordResetTokenAsync(UserModel user);
	Task<string> GenerateTwoFactorTokenAsync(UserModel user, string tokenProvider);
	Task<string> GenerateUserTokenAsync(UserModel user, string tokenProvider, string purpose);
	Task<int> GetAccessFailedCountAsync(UserModel user);
	Task<string> GetAuthenticationTokenAsync(UserModel user, string loginProvider, string tokenName);
	Task<string> GetAuthenticatorKeyAsync(UserModel user);
	Task<IList<Claim>> GetClaimsAsync(UserModel user);
	Task<string> GetEmailAsync(UserModel user);
	Task<bool> GetLockoutEnabledAsync(UserModel user);
	Task<DateTimeOffset?> GetLockoutEndDateAsync(UserModel user);
	Task<IList<UserLoginInfo>> GetLoginsAsync(UserModel user);
	Task<string> GetPhoneNumberAsync(UserModel user);
	Task<IList<string>> GetRolesAsync(UserModel user);
	Task<string> GetSecurityStampAsync(UserModel user);
	Task<bool> GetTwoFactorEnabledAsync(UserModel user);
	Task<UserModel> GetUserAsync(ClaimsPrincipal principal);
	string GetUserId(ClaimsPrincipal principal);
	Task<string> GetUserIdAsync(UserModel user);
	string GetUserName(ClaimsPrincipal principal);
	Task<string> GetUserNameAsync(UserModel user);
	Task<IList<UserModel>> GetUsersForClaimAsync(Claim claim);
	Task<IList<UserModel>> GetUsersInRoleAsync(string roleName);
	Task<IList<string>> GetValidTwoFactorProvidersAsync(UserModel user);
	Task<bool> HasPasswordAsync(UserModel user);
	Task<bool> IsEmailConfirmedAsync(UserModel user);
	Task<bool> IsInRoleAsync(UserModel user, string role);
	Task<bool> IsLockedOutAsync(UserModel user);
	Task<bool> IsPhoneNumberConfirmedAsync(UserModel user);
	string NormalizeEmail(string email);
	string NormalizeName(string name);
	Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(UserModel user, string code);
	void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<UserModel> provider);
	Task<IdentityResult> RemoveAuthenticationTokenAsync(UserModel user, string loginProvider, string tokenName);
	Task<IdentityResult> RemoveClaimAsync(UserModel user, Claim claim);
	Task<IdentityResult> RemoveClaimsAsync(UserModel user, IEnumerable<Claim> claims);
	Task<IdentityResult> RemoveFromRoleAsync(UserModel user, string role);
	Task<IdentityResult> RemoveFromRolesAsync(UserModel user, IEnumerable<string> roles);
	Task<IdentityResult> RemoveLoginAsync(UserModel user, string loginProvider, string providerKey);
	Task<IdentityResult> RemovePasswordAsync(UserModel user);
	Task<IdentityResult> ReplaceClaimAsync(UserModel user, Claim claim, Claim newClaim);
	Task<IdentityResult> ResetAccessFailedCountAsync(UserModel user);
	Task<IdentityResult> ResetAuthenticatorKeyAsync(UserModel user);
	Task<IdentityResult> ResetPasswordAsync(UserModel user, string token, string newPassword);
	Task<IdentityResult> SetAuthenticationTokenAsync(UserModel user, string loginProvider, string tokenName, string tokenValue);
	Task<IdentityResult> SetEmailAsync(UserModel user, string email);
	Task<IdentityResult> SetLockoutEnabledAsync(UserModel user, bool enabled);
	Task<IdentityResult> SetLockoutEndDateAsync(UserModel user, DateTimeOffset? lockoutEnd);
	Task<IdentityResult> SetPhoneNumberAsync(UserModel user, string phoneNumber);
	Task<IdentityResult> SetTwoFactorEnabledAsync(UserModel user, bool enabled);
	Task<IdentityResult> SetUserNameAsync(UserModel user, string userName);
	Task<IdentityResult> UpdateAsync(UserModel user);
	Task UpdateNormalizedEmailAsync(UserModel user);
	Task UpdateNormalizedUserNameAsync(UserModel user);
	Task<IdentityResult> UpdateSecurityStampAsync(UserModel user);
	Task<bool> VerifyChangePhoneNumberTokenAsync(UserModel user, string token, string phoneNumber);
	Task<bool> VerifyTwoFactorTokenAsync(UserModel user, string tokenProvider, string token);
	Task<bool> VerifyUserTokenAsync(UserModel user, string tokenProvider, string purpose, string token);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
