using Application.Interfaces.Infrastructure.Repositories.BaseTypes;
using Domain.Entities.Finance;

namespace Application.Interfaces.Infrastructure.Repositories;

/// <summary>
/// The transaction repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface ITransactionRepository : IIdentityRepository<Transaction>
{
}
