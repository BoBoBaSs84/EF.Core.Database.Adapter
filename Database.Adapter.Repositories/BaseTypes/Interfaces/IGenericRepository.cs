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
	IQueryable<TEntity> GetAll(bool trackChanges = false);

	/// <summary>
	/// Should find entries of an entity by a certain condition.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A list of entries.</returns>
	IQueryable<TEntity> GetManyByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);

	/// <summary>
	/// Should find an entry of an entity by its primary key.
	/// </summary>
	/// <param name="Id">The primary key of the entity.</param>
	/// <returns>One entry of an entity.</returns>
	TEntity GetById(Guid Id);

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
	/// <param name="id">The identifier of the entity.</param>
	void Delete(Guid id);

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
	void Create(TEntity entity);

	/// <summary>
	/// Should create multiple entries of an entity.
	/// </summary>
	/// <param name="entities">The entries of an entity to create.</param>
	void CreateRange(IEnumerable<TEntity> entities);

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
