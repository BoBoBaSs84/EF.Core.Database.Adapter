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
	/// Should find all entries within a entity.
	/// </summary>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A list of entries.</returns>
	IQueryable<TEntity> FindAll(bool trackChanges = false);

	/// <summary>
	/// Should find entries within a entity by a certein condition.
	/// </summary>
	/// <param name="expression">The search condition.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <returns>A list of entries.</returns>
	IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false);

	/// <summary>
	/// Should find an entity by its primary key.
	/// </summary>
	/// <param name="Id">The primary key of the entity.</param>
	/// <returns>One entry of an entity.</returns>
	TEntity FindById(Guid Id);

	/// <summary>
	/// Should delete an entry of an entity.
	/// </summary>
	/// <param name="entity">The entry of an entity to delete.</param>
	void Delete(TEntity entity);

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
