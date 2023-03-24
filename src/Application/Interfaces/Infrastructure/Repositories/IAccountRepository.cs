using Application.Interfaces.Infrastructure.Repositories.BaseTypes;
using Domain.Entities.Finance;

namespace Application.Interfaces.Infrastructure.Repositories;

/// <summary>
/// The account repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface IAccountRepository : IIdentityRepository<Account>
{
}
