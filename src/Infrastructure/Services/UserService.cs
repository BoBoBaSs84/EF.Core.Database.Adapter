using Application.Interfaces.Infrastructure.Services;

using BB84.Home.Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

/// <summary>
/// The user service class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="UserManager{TUser}"/> class
/// </remarks>
/// <remarks>
/// Initializes a new instance of the user service class.
/// </remarks>
/// <inheritdoc/>
[ExcludeFromCodeCoverage(Justification = "Wrapper class.")]
internal sealed class UserService(IUserStore<UserEntity> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<UserEntity> passwordHasher, IEnumerable<IUserValidator<UserEntity>> userValidators, IEnumerable<IPasswordValidator<UserEntity>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<UserEntity>> logger) : UserManager<UserEntity>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger), IUserService
{ }
