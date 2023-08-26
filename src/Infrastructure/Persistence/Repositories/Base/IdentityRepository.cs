using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

using Domain.Common.EntityBaseTypes;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The identity repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEntity">The identity entity.</typeparam>
internal abstract class IdentityRepository<TEntity> : GenericRepository<TEntity>, IIdentityRepository<TEntity> where TEntity : IdentityModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="IdentityRepository{TEntity}"/> class.
	/// </summary>
	/// <inheritdoc/>
	protected IdentityRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<TEntity?> GetByIdAsync(int id, bool ignoreQueryFilters = false, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		IQueryable<TEntity> query = !trackChanges ? dbSet.AsNoTracking() : dbSet;

		query = query.Where(x => x.Id.Equals(id));

		if (ignoreQueryFilters)
			query = query.IgnoreQueryFilters();

		return await query.SingleOrDefaultAsync(cancellationToken);
	}


	public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, bool ignoreQueryFilters = false, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		IQueryable<TEntity> query = !trackChanges ? dbSet.AsNoTracking() : dbSet;

		query = query.Where(x => ids.Contains(x.Id));

		if (ignoreQueryFilters)
			query = query.IgnoreQueryFilters();

		return await query.ToListAsync(cancellationToken);
	}
}
