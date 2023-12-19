using System.Linq.Expressions;

using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The generic repository class.
/// </summary>
/// <remarks>
/// Implemnts the members of the <see cref="IGenericRepository{TEntity}"/> interface.
/// </remarks>
/// <typeparam name="TEntity">The entity to work with.</typeparam>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, generic repository.")]
internal abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
	protected DbContext _dbContext;
	protected DbSet<TEntity> _dbSet;

	/// <summary>
	/// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
	/// </summary>
	/// <remarks>
	/// Any context that inherits from <see cref="DbContext"/> should work.
	/// </remarks>
	/// <param name="dbContext">The database context to work with.</param>	
	protected GenericRepository(DbContext dbContext)
	{
		_dbContext = dbContext;
		_dbSet = dbContext.Set<TEntity>();
	}

	public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
		=> await _dbSet.AddAsync(entity, cancellationToken);

	public async Task CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		=> await _dbSet.AddRangeAsync(entities, cancellationToken);

	public async Task<IEnumerable<TEntity>> GetAllAsync(
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default)
	{
		IQueryable<TEntity> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

		if (ignoreQueryFilters)
			query = query.IgnoreQueryFilters();

		return await query.ToListAsync(cancellationToken);
	}

	public async Task<IEnumerable<TEntity>> GetManyByConditionAsync(
		Expression<Func<TEntity, bool>>? expression = null,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
		int? take = null,
		int? skip = null,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties)
	{
		IQueryable<TEntity> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

		if (expression is not null)
			query = query.Where(expression);

		if (queryFilter is not null)
			query = queryFilter(query);

		if (ignoreQueryFilters)
			query = query.IgnoreQueryFilters();

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
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		bool trackChanges = false,
		CancellationToken cancellationToken = default,
		params string[] includeProperties)
	{
		IQueryable<TEntity> query = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;

		if (expression is not null)
			query = query.Where(expression);

		if (queryFilter is not null)
			query = queryFilter(query);

		if (ignoreQueryFilters)
			query = query.IgnoreQueryFilters();

		if (includeProperties.Length > 0)
			query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));

		return await query.SingleOrDefaultAsync(cancellationToken);
	}

	public Task DeleteAsync(TEntity entity)
	{
		_dbSet.Remove(entity);
		return Task.CompletedTask;
	}

	public Task DeleteAsync(IEnumerable<TEntity> entities)
	{
		_dbSet.RemoveRange(entities);
		return Task.CompletedTask;
	}

	public Task UpdateAsync(TEntity entity)
	{
		_dbSet.Update(entity);
		return Task.CompletedTask;
	}

	public Task UpdateAsync(IEnumerable<TEntity> entities)
	{
		_dbSet.UpdateRange(entities);
		return Task.CompletedTask;
	}

	public async Task<int> GetCountAsync(
		Expression<Func<TEntity, bool>>? expression = null,
		Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFilter = null,
		bool ignoreQueryFilters = false,
		CancellationToken cancellationToken = default)
	{
		IQueryable<TEntity> query = _dbSet.AsNoTracking();

		if (expression is not null)
			query = query.Where(expression);

		if (queryFilter is not null)
			query = queryFilter(query);

		if (ignoreQueryFilters)
			query = query.IgnoreQueryFilters();

		return await query.CountAsync(cancellationToken);
	}

	public async Task<int> GetTotalCountAsync(bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
	{
		IQueryable<TEntity> query = _dbSet.AsNoTracking();

		if (ignoreQueryFilters)
			query = query.IgnoreQueryFilters();

		return await query.CountAsync(cancellationToken);
	}
}
