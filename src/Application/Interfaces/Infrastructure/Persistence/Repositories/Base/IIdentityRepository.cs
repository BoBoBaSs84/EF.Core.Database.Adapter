using Domain.Common.EntityBaseTypes;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The identity repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEntity">The identity entity work with.</typeparam>
public interface IIdentityRepository<TEntity> : IGenericRepository<TEntity> where TEntity : IdentityModel
{
	/// <summary>
	/// Should fetch a collection of entites by their primary keys.
	/// </summary>
	/// <param name="ids">The primary key of the entities.</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A collection of entities.</returns>
	Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges = false, CancellationToken cancellationToken = default);
}
