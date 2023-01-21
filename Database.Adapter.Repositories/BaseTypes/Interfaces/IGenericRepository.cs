using System.Linq.Expressions;

namespace Database.Adapter.Repositories.BaseTypes.Interfaces;

/// <summary>
/// The generic repository interface.
/// </summary>
/// <remarks>
/// The mother interface of all repositories.
/// </remarks>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
	/// <summary>
	/// Should find all entries of an entity.
	/// </summary>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A list of entries.</returns>
	Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default);
	
	/// <summary>
	/// Should find a collection of entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfil to be returned.</param>
	/// <param name="orderBy">The function used to order the entities.</param>
	/// <param name="top">The number of records to limit the results to.</param>
	/// <param name="skip">The number of records to skip.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the collection.</param>
	/// <returns>A collection of entities.</returns>
	Task<IEnumerable<TEntity>> GetManyByConditionAsync(
		Expression<Func<TEntity, bool>> expression,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		int? top = null,
		int? skip = null,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties);
	
	/// <summary>
	/// Should find an entry of an entity by its primary key.
	/// </summary>
	/// <param name="id">The primary key of the entity.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>One entry of an entity.</returns>
	Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	
	/// <summary>
	/// Should find an entry of an entity by its primary key.
	/// </summary>
	/// <param name="id">The primary key of the entity.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>One entry of an entity.</returns>
	Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	
	/// <summary>
	/// Should find an entry of an entity by a certain condition.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the entity.</param>
	/// <returns>One entry of an entity.</returns>
	Task<TEntity> GetByConditionAsync(
		Expression<Func<TEntity, bool>> expression,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties
		);

	/// <summary>
	/// Should delete an entity.
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(TEntity entity);

	/// <summary>
	/// Should delete an entity by its identifier
	/// </summary>
	/// <param name="id">The identifier of the entity to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(Guid id);

	/// <summary>
	/// Should delete an entity by its identifier
	/// </summary>
	/// <param name="id">The identifier of the entity to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(int id);

	/// <summary>
	/// Should delete one or many entities by a certain condition.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfil to be deleted.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(Expression<Func<TEntity, bool>> expression);

	/// <summary>
	/// Should delete multiple entities.
	/// </summary>
	/// <param name="entities">The entities to delete.</param>
	/// <returns><see cref="Task"/></returns>
	Task DeleteAsync(IEnumerable<TEntity> entities);

	/// <summary>
	/// Should create an entity.
	/// </summary>
	/// <param name="entity">The entity to create.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task"/></returns>
	Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should create multiple entities.
	/// </summary>
	/// <param name="entities">The entities to create.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="Task"/></returns>
	Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should update an entity.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <returns><see cref="Task"/></returns>
	Task UpdateAsync(TEntity entity);

	/// <summary>
	/// Should update multiple entities.
	/// </summary>
	/// <param name="entities">The entities to update.</param>	
	/// <returns><see cref="Task"/></returns>
	Task UpdateAsync(IEnumerable<TEntity> entities);
}
