using System.Linq.Expressions;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The generic repository interface.
/// </summary>
/// <typeparam name="T">The entity to work with.</typeparam>
public interface IGenericRepository<T> where T : class
{
	/// <summary>
	/// Creates an entity.
	/// </summary>
	/// <param name="entity">The entity to create.</param>
	void Create(T entity);

	/// <summary>
	/// Creates multiple entities.
	/// </summary>
	/// <param name="entities">The entities to create.</param>
	void Create(IEnumerable<T> entities);

	/// <summary>
	/// Creates an entity.
	/// </summary>
	/// <param name="entity">The entity to create.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task"/></returns>
	Task CreateAsync(T entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates multiple entities.
	/// </summary>
	/// <param name="entities">The entities to create.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task"/></returns>
	Task CreateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an entity.
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	void Delete(T entity);

	/// <summary>
	/// Deletes multiple entities.
	/// </summary>
	/// <param name="entities">The entities to delete.</param>
	void Delete(IEnumerable<T> entities);

	/// <summary>
	/// Deletes an entity.
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(T entity);

	/// <summary>
	/// Deletes multiple entities.
	/// </summary>
	/// <param name="entities">The entities to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(IEnumerable<T> entities);

	/// <summary>
	/// Returns the number of all entities.
	/// </summary>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <returns>The number entities.</returns>
	int Count(bool ignoreQueryFilters = false);

	/// <summary>
	/// Returns the number entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfill to be counted.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <returns>The number entities.</returns>
	int Count(
		Expression<Func<T, bool>>? expression = null,
		Func<IQueryable<T>, IQueryable<T>>? queryFilter = null,
		bool ignoreQueryFilters = false
		);

	/// <summary>
	/// Returns the number of all entities.
	/// </summary>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The number entities.</returns>
	Task<int> CountAsync(bool ignoreQueryFilters = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns the number entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfill to be counted.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The number of entities in dependence to the expression.</returns>
	Task<int> CountAsync(
		Expression<Func<T, bool>>? expression = null,
		Func<IQueryable<T>, IQueryable<T>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Returns all entities.
	/// </summary>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <returns>A colection of entities.</returns>
	IEnumerable<T> GetAll(bool ignoreQueryFilters = false, bool trackChanges = false);

	/// <summary>
	/// Returns all entities.
	/// </summary>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A colection of entities.</returns>
	Task<IEnumerable<T>> GetAllAsync(
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Returns a collection entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfill to be returned.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="orderBy">The function used to order the entities.</param>
	/// <param name="take">The number of records to limit the results to.</param>
	/// <param name="skip">The number of records to skip.</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the collection.</param>
	/// <returns>A collection of entities.</returns>
	IEnumerable<T> GetManyByCondition(
		Expression<Func<T, bool>>? expression = null,
		Func<IQueryable<T>, IQueryable<T>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		int? take = null,
		int? skip = null,
		bool trackChanges = false,
		params string[] includeProperties
		);

	/// <summary>
	/// Returns a collection entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfill to be returned.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="orderBy">The function used to order the entities.</param>
	/// <param name="take">The number of records to limit the results to.</param>
	/// <param name="skip">The number of records to skip.</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the collection.</param>
	/// <returns>A collection of entities.</returns>
	Task<IEnumerable<T>> GetManyByConditionAsync(
		Expression<Func<T, bool>>? expression = null,
		Func<IQueryable<T>, IQueryable<T>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
		int? take = null,
		int? skip = null,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties
		);

	/// <summary>
	/// Returns an entity by a certain condition.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the entity.</param>
	/// <returns>The found entity or <see langword="null"/>.</returns>
	T? GetByCondition(
		Expression<Func<T, bool>> expression,
		Func<IQueryable<T>, IQueryable<T>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		params string[] includeProperties
		);

	/// <summary>
	/// Returns an entity by a certain condition.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the entity.</param>
	/// <returns>The found entity or <see langword="null"/>.</returns>
	Task<T?> GetByConditionAsync(
		Expression<Func<T, bool>> expression,
		Func<IQueryable<T>, IQueryable<T>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties
		);

	/// <summary>
	/// Updates an entity.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	void Update(T entity);

	/// <summary>
	/// Updates multiple entities.
	/// </summary>
	/// <param name="entities">The entities to update.</param>
	void Update(IEnumerable<T> entities);

	/// <summary>
	/// Updates an entity.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <returns><see cref="Task"/></returns>
	Task UpdateAsync(T entity);

	/// <summary>
	/// Updates multiple entities.
	/// </summary>
	/// <param name="entities">The entities to update.</param>	
	/// <returns><see cref="Task"/></returns>
	Task UpdateAsync(IEnumerable<T> entities);
}
