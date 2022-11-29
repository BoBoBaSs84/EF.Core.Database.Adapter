using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Database.Adapter.Repositories.BaseTypes;

/// <summary>
/// The generic repository class.
/// </summary>
/// <remarks>
/// Implemnts the members of the <see cref="IGenericRepository{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
	private readonly DbContext dbContext;
	private readonly DbSet<TEntity> dbSet;

	/// <summary>
	/// The standard constructor.
	/// </summary>
	/// <remarks>
	/// Any context that inherits from <see cref="DbContext"/> should work.
	/// </remarks>
	/// <param name="dbContext">The context to work with.</param>	
	public GenericRepository(DbContext dbContext)
	{
		this.dbContext = dbContext;
		dbSet = dbContext.Set<TEntity>();
	}

	/// <inheritdoc/>
	public IQueryable<TEntity> GetAll(bool trackChanges = false) =>
		!trackChanges ? dbSet.AsNoTracking() : dbContext.Set<TEntity>();

	/// <inheritdoc/>
	public IQueryable<TEntity> GetManyByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false) =>
		!trackChanges ? dbSet.Where(expression).AsNoTracking() : dbSet.Where(expression);

	/// <inheritdoc/>
	public TEntity GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false) =>
		!trackChanges ? dbSet.Where(expression).AsNoTracking().SingleOrDefault()! : dbSet.Where(expression).SingleOrDefault()!;

	/// <inheritdoc/>
	public TEntity GetById(Guid id) => dbSet.Find(id)!;

	/// <inheritdoc/>
	public void Delete(TEntity entity) => dbSet.Remove(entity);

	/// <inheritdoc/>
	public void Delete(Guid id)
	{
		TEntity entity = dbSet.Find(id)!;
		if (entity is not null)
			Delete(entity);
	}

	/// <inheritdoc/>
	public void Delete(Expression<Func<TEntity, bool>> expression)
	{
		IQueryable<TEntity> entities = dbSet.Where(expression);
		DeleteRange(entities);
	}

	/// <inheritdoc/>
	public void DeleteRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);

	/// <inheritdoc/>
	public void Create(TEntity entity) => dbSet.Add(entity);

	/// <inheritdoc/>
	public void CreateRange(IEnumerable<TEntity> entities) => dbSet.AddRange(entities);

	/// <inheritdoc/>
	public void Update(TEntity entity) => dbSet.Update(entity);

	/// <inheritdoc/>
	public void UpdateRange(IEnumerable<TEntity> entities) => dbSet.UpdateRange(entities);
}
