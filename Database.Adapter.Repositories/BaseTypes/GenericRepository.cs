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
internal abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
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
	public IEnumerable<TEntity> GetAll(bool trackChanges = false) =>
		!trackChanges ? dbSet.AsNoTracking() : dbContext.Set<TEntity>();

	/// <inheritdoc/>
	public IEnumerable<TEntity> GetManyByCondition(
		Expression<Func<TEntity, bool>> expression,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		int? top = null,
		int? skip = null,
		bool trackChanges = false,
		params string[] includeProperties)
	{
		IQueryable<TEntity> query = !trackChanges ? dbSet.AsNoTracking() : dbSet;
		if (expression is not null)
			query = query.Where(expression);
		if (includeProperties.Length > 0)
			query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
		if (orderBy is not null)
			query = orderBy(query);
		if (skip.HasValue)
			query = query.Skip(skip.Value);
		if (top.HasValue)
			query = query.Take(top.Value);
		return query.ToList();
	}

	/// <inheritdoc/>
	public TEntity GetByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges = false) =>
		!trackChanges ? dbSet.Where(expression).AsNoTracking().SingleOrDefault()! : dbSet.Where(expression).SingleOrDefault()!;

	/// <inheritdoc/>
	public TEntity GetById(Guid id) => dbSet.Find(id)!;

	/// <inheritdoc/>
	public TEntity GetById(int id) => dbSet.Find(id)!;

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
	public void Delete(int id)
	{
		TEntity entity = dbSet.Find(id)!;
		if (entity is not null)
			Delete(entity);
	}

	/// <inheritdoc/>
	public void Delete(Expression<Func<TEntity, bool>> expression)
	{
		IQueryable<TEntity> entities = dbSet.Where(expression);
		Delete(entities);
	}

	/// <inheritdoc/>
	public void Delete(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);

	/// <inheritdoc/>
	public TEntity Create(TEntity entity)
	{
		_ = dbSet.Add(entity);
		return entity;

	}

	/// <inheritdoc/>
	public IEnumerable<TEntity> Create(IEnumerable<TEntity> entities)
	{
		dbSet.AddRange(entities);
		return entities;
	}

	/// <inheritdoc/>
	public void Update(TEntity entity) => dbSet.Update(entity);

	/// <inheritdoc/>
	public void Update(IEnumerable<TEntity> entities) => dbSet.UpdateRange(entities);
}
