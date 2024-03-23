using Domain.Interfaces.Models;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The enumerator repository interface.
/// </summary>
/// <typeparam name="T">
/// Must implement the <see cref="IEnumeratorModel"/> interface.
/// </typeparam>
public interface IEnumeratorRepository<T> : IGenericRepository<T> where T : class, IEnumeratorModel
{
	/// <summary>
	/// Returns an entity by its primary key.
	/// </summary>
	/// <param name="id">The primary key of the entity.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <returns>The entity or <see langword="null"/>.</returns>
	T? GetById(
		int id,
		bool ignoreQueryFilters = false,
		bool trackChanges = false
		);

	/// <summary>
	/// Returns an entity by its primary key.
	/// </summary>
	/// <param name="id">The primary key of the entity.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The entity or <see langword="null"/>.</returns>
	Task<T?> GetByIdAsync(
		int id,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Returns a collection of entites by their primary keys.
	/// </summary>
	/// <param name="ids">The primary key of the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <returns>A collection of entities.</returns>
	IEnumerable<T> GetByIds(
		IEnumerable<int> ids,
		bool ignoreQueryFilters = false,
		bool trackChanges = false
		);

	/// <summary>
	/// Returns a collection of entites of type <typeparamref name="T"/> by their primary keys.
	/// </summary>
	/// <param name="ids">The primary key of the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A collection of entities.</returns>
	Task<IEnumerable<T>> GetByIdsAsync(
		IEnumerable<int> ids,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Returns an entity by its name.
	/// </summary>
	/// <param name="name">The name of the entity.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <returns>The entity or <see langword="null"/>.</returns>
	T? GetByName(
		string name,
		bool ignoreQueryFilters = false,
		bool trackChanges = false
		);

	/// <summary>
	/// Returns an entity by its name.
	/// </summary>
	/// <param name="name">The name of the entity.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The entity or <see langword="null"/>.</returns>
	Task<T?> GetByNameAsync(
		string name,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default
		);
}
