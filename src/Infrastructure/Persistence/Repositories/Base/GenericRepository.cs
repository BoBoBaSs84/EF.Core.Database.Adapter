using Application.Interfaces.Infrastructure.Repositories.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The generic repository class.
/// </summary>
/// <remarks>
/// Implemnts the members of the <see cref="IGenericRepository{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
internal abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
	protected DbContext dbContext;
	protected DbSet<TEntity> dbSet;

	/// <inheritdoc/>
	public int TotalCount { get; private set; }

	/// <inheritdoc/>
	public int QueryCount { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
	/// </summary>
	/// <remarks>
	/// Any context that inherits from <see cref="DbContext"/> should work.
	/// </remarks>
	/// <param name="dbContext">The database context to work with.</param>	
	public GenericRepository(DbContext dbContext)
	{
		this.dbContext = dbContext;
		dbSet = dbContext.Set<TEntity>();
		TotalCount = dbSet.Count();
	}

	public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default) =>
		await dbSet.AddAsync(entity, cancellationToken);

	public async Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
		await dbSet.AddRangeAsync(entities, cancellationToken);

	public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await (!trackChanges ? dbSet.AsNoTracking().ToListAsync(cancellationToken) : dbSet.ToListAsync(cancellationToken));

	public async Task<IEnumerable<TEntity>> GetManyByConditionAsync(
		Expression<Func<TEntity, bool>>? expression = null,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? filterBy = null,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		int? take = null,
		int? skip = null,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties)
	{
		IQueryable<TEntity> query = !trackChanges ? dbSet.AsNoTracking() : dbSet;
		
		if (expression is not null)
			query = query.Where(expression);
		
		if (filterBy is not null)
			query = filterBy(query);
		
		QueryCount = await query.CountAsync(cancellationToken);
		
		if (includeProperties.Length > 0)
			query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
		
		if (orderBy is not null)
			query = orderBy(query);
		
		if (skip.HasValue)
			query = query.Skip(skip.Value);
		
		if (take.HasValue)
			query = query.Take(take.Value);
		
		return await query.ToListAsync(cancellationToken);
	}

	public async Task<TEntity?> GetByConditionAsync(
		Expression<Func<TEntity, bool>> expression,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties)
	{
		IQueryable<TEntity> query = !trackChanges ? dbSet.AsNoTracking() : dbSet;
		
		if (expression is not null)
			query = query.Where(expression);
		
		if (includeProperties.Length > 0)
			query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
		
		return await query.SingleOrDefaultAsync(cancellationToken);
	}

	public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
		await dbSet.FindAsync(keyValues: new object[] { id }, cancellationToken: cancellationToken);

	public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
		await dbSet.FindAsync(keyValues: new object[] { id }, cancellationToken: cancellationToken);

	public Task DeleteAsync(TEntity entity)
	{
		_ = dbSet.Remove(entity);
		return Task.CompletedTask;
	}

	public async Task DeleteAsync(Guid id)
	{
		TEntity? entity = await dbSet.FindAsync(id);
		if (entity is not null)
			await DeleteAsync(entity);
	}

	public async Task DeleteAsync(int id)
	{
		TEntity? entity = await dbSet.FindAsync(id);
		if (entity is not null)
			await DeleteAsync(entity);
	}

	public Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
	{
		IQueryable<TEntity> entities = dbSet.Where(expression);
		dbSet.RemoveRange(entities);
		return Task.CompletedTask;
	}

	public Task DeleteAsync(IEnumerable<TEntity> entities)
	{
		dbSet.RemoveRange(entities);
		return Task.CompletedTask;
	}

	public Task UpdateAsync(TEntity entity)
	{
		_ = dbSet.Update(entity);
		return Task.CompletedTask;
	}

	public Task UpdateAsync(IEnumerable<TEntity> entities)
	{
		dbSet.UpdateRange(entities);
		return Task.CompletedTask;
	}
}
