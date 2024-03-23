using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

using Domain.Interfaces.Models;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The <see langword="abstract"/> identity repository class.
/// </summary>
/// <typeparam name="T">
/// Must implement the <see cref="IIdentityModel"/> interface.
/// </typeparam>
/// <inheritdoc/>
internal abstract class IdentityRepository<T>(DbContext dbContext) : GenericRepository<T>(dbContext), IIdentityRepository<T> where T : class, IIdentityModel
{
	public T? GetById(Guid id, bool ignoreQueryFilters = false, bool trackChanges = false)
	{
		IQueryable<T> query = PrepareQuery(
			x => x.Id.Equals(id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return query.SingleOrDefault();
	}

	public async Task<T?> GetByIdAsync(Guid id, bool ignoreQueryFilters = false, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		IQueryable<T> query = PrepareQuery(
			x => x.Id.Equals(id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return await query.SingleOrDefaultAsync(cancellationToken);
	}

	public IEnumerable<T> GetByIds(IEnumerable<Guid> ids, bool ignoreQueryFilters = false, bool trackChanges = false)
	{
		IQueryable<T> query = PrepareQuery(
			x => ids.Contains(x.Id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return [.. query];
	}

	public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, bool ignoreQueryFilters = false, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		IQueryable<T> query = PrepareQuery(
			x => ids.Contains(x.Id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return await query.ToListAsync(cancellationToken);
	}
}
