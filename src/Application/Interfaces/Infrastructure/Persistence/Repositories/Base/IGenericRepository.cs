﻿using System.Linq.Expressions;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The generic repository interface.
/// </summary>
/// <remarks>
/// The mother of all repository interfaces.
/// </remarks>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
	/// <summary>
	/// Returns the number of entities in dependence to the expression.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfill to be counted.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The number of entities in dependence to the expression.</returns>
	Task<int> GetCountAsync(
		Expression<Func<TEntity, bool>>? expression = null,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Returns the number of all entities.
	/// </summary>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The number of all entities.</returns>
	Task<int> GetCountAsync(
		bool ignoreQueryFilters = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Should find all entries of the <typeparamref name="TEntity"/> entity.
	/// </summary>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A list of entries.</returns>
	Task<IEnumerable<TEntity>> GetAllAsync(
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default
		);

	/// <summary>
	/// Should find a collection of <typeparamref name="TEntity"/> entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfill to be returned.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="orderBy">The function used to order the entities.</param>
	/// <param name="take">The number of records to limit the results to.</param>
	/// <param name="skip">The number of records to skip.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the collection.</param>
	/// <returns>A collection of entities.</returns>
	Task<IEnumerable<TEntity>> GetManyByConditionAsync(
		Expression<Func<TEntity, bool>>? expression = null,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		int? take = null,
		int? skip = null,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties
		);

	/// <summary>
	/// Should find an entry of an <typeparamref name="TEntity"/> entity by a certain condition.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="queryFilter">The function used to filter the entities.</param>
	/// <param name="ignoreQueryFilters">Should model-level entity query filters be applied?</param>
	/// <param name="trackChanges">Should the fetched entity be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the entity.</param>
	/// <returns>One entry of an entity.</returns>
	Task<TEntity?> GetByConditionAsync(
		Expression<Func<TEntity, bool>> expression,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties
		);

	/// <summary>
	/// Should delete an <typeparamref name="TEntity"/> entity.
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(TEntity entity);

	/// <summary>
	/// Should delete multiple <typeparamref name="TEntity"/> entities.
	/// </summary>
	/// <param name="entities">The entities to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(IEnumerable<TEntity> entities);

	/// <summary>
	/// Should create an <typeparamref name="TEntity"/> entity.
	/// </summary>
	/// <param name="entity">The entity to create.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task"/></returns>
	Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should create multiple <typeparamref name="TEntity"/> entities.
	/// </summary>
	/// <param name="entities">The entities to create.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task"/></returns>
	Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should update an <typeparamref name="TEntity"/> entity.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <returns><see cref="Task"/></returns>
	Task UpdateAsync(TEntity entity);

	/// <summary>
	/// Should update multiple <typeparamref name="TEntity"/> entities.
	/// </summary>
	/// <param name="entities">The entities to update.</param>	
	/// <returns><see cref="Task"/></returns>
	Task UpdateAsync(IEnumerable<TEntity> entities);
}
