using DA.Domain.Models.Identity;
using DA.Infrastructure.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DA.Infrastructure.Application;

/// <summary>
/// The user service class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="UserManager{TUser}"/> class.
/// </remarks>
internal sealed class UserService : UserManager<User>, IUserService
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserService"/> class.
	/// </summary>
	/// <inheritdoc/>
	public UserService(
		IUserStore<User> store,
		IOptions<IdentityOptions> optionsAccessor,
		IPasswordHasher<User> passwordHasher,
		IEnumerable<IUserValidator<User>> userValidators,
		IEnumerable<IPasswordValidator<User>> passwordValidators,
		ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
		IServiceProvider services, ILogger<UserManager<User>> logger)
		: base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
	{
	}
}
