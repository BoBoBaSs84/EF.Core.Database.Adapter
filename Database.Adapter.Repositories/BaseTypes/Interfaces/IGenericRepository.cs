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
	/// <returns>A list of entries.</returns>
	IEnumerable<TEntity> GetAll(bool trackChanges = false);

	/// <summary>
	/// Should find a collection of entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A list of entries.</returns>
	IEnumerable<TEntity> GetManyByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);

	/// <summary>
	/// Should find a collection of entities based on the specified criteria.
	/// </summary>
	/// <param name="expression">The condition the entities must fulfil to be returned.</param>
	/// <param name="orderBy">The function used to order the entities.</param>
	/// <param name="top">The number of records to limit the results to.</param>
	/// <param name="skip">The number of records to skip.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="includeProperties">Any other navigation properties to include when returning the collection.</param>
	/// <returns>A collection of entities.</returns>
	IEnumerable<TEntity> GetManyByCondition(
		Expression<Func<TEntity, bool>> expression,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		int? top = null,
		int? skip = null,
		bool trackChanges = false,
		params string[] includeProperties);

	/// <summary>
	/// Should find an entry of an entity by its primary key.
	/// </summary>
	/// <remarks>
	/// <paramref name="id"/> is of type <see cref="Guid"/>
	/// </remarks>
	/// <param name="id">The primary key of the entity.</param>
	/// <returns>One entry of an entity.</returns>
	TEntity GetById(Guid id);

	/// <summary>
	/// Should find an entry of an entity by its primary key.
	/// </summary>
	/// <remarks>
	/// <paramref name="id"/> is of type <see cref="int"/>
	/// </remarks>
	/// <param name="id">The primary key of the entity.</param>
	/// <returns>One entry of an entity.</returns>
	TEntity GetById(int id);

	/// <summary>
	/// Should find an entry of an entity by a certain condition.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns></returns>
	TEntity GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);

	/// <summary>
	/// Should delete an entry of an entity.
	/// </summary>
	/// <param name="entity">The entry of an entity to delete.</param>
	void Delete(TEntity entity);

	/// <summary>
	/// Should delete an entry of an entity by its identifier.
	/// </summary>
	/// <remarks>
	/// <paramref name="id"/> is of type <see cref="Guid"/>
	/// </remarks>
	/// <param name="id">The identifier of the entity.</param>
	void Delete(Guid id);

	/// <summary>
	/// Should delete an entry of an entity by its identifier.
	/// </summary>
	/// <remarks>
	/// <paramref name="id"/> is of type <see cref="int"/>
	/// </remarks>
	/// <param name="id">The identifier of the entity.</param>
	void Delete(int id);

	/// <summary>
	/// Should delete one entry or many entries of an entity by a certain condition.
	/// </summary>
	/// <param name="expression"></param>
	void Delete(Expression<Func<TEntity, bool>> expression);

	/// <summary>
	/// Should delete multiple entries of an entity.
	/// </summary>
	/// <param name="entities">The entries of an entity to delete.</param>
	void DeleteRange(IEnumerable<TEntity> entities);

	/// <summary>
	/// Should create an entry of an entity.
	/// </summary>
	/// <param name="entity">The entry of an entity to create.</param>
	/// <returns>The created entity.</returns>
	TEntity Create(TEntity entity);

	/// <summary>
	/// Should create multiple entries of an entity.
	/// </summary>
	/// <param name="entities">The entries of an entity to create.</param>
	/// <returns>The created entities.</returns>
	IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);

	/// <summary>
	/// Should update an entry of an entity.
	/// </summary>
	/// <param name="entity">The entry of an entity to update.</param>
	void Update(TEntity entity);

	/// <summary>
	/// Should update multiple entries of an entity.
	/// </summary>
	/// <param name="entities">The entries of an entity to update.</param>	
	void UpdateRange(IEnumerable<TEntity> entities);
}
