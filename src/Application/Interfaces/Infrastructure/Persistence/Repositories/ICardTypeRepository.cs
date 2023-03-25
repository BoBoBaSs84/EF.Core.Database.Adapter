using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;
using Domain.Entities.Enumerator;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The card type repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface ICardTypeRepository : IGenericRepository<CardType>
{
}
