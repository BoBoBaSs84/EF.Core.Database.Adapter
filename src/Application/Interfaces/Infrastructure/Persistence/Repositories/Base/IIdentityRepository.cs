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
	/// Should fetch an entity of type <typeparamref name="TEntity"/> by its primary key.
	/// </summary>
	/// <param name="id">The primary key of the entity.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>An entity.</returns>
	Task<TEntity?> GetByIdAsync(
		int id,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Should fetch a collection of entites of type <typeparamref name="TEntity"/> by their primary keys.
	/// </summary>
	/// <param name="ids">The primary key of the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A collection of entities.</returns>
	Task<IEnumerable<TEntity>> GetByIdsAsync(
		IEnumerable<int> ids,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default
		);
}
