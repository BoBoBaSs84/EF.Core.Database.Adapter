using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

using Domain.Interfaces.Models;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

/// <summary>
/// The enumerator repository class.
/// </summary>
/// <typeparam name="T">
/// Must implement the <see cref="IEnumeratorModel"/> interface.
/// </typeparam>
/// <inheritdoc/>
internal abstract class EnumeratorRepository<T>(DbContext dbContext) : GenericRepository<T>(dbContext), IEnumeratorRepository<T> where T : class, IEnumeratorModel
{
	public T? GetById(int id, bool ignoreQueryFilters = false, bool trackChanges = false)
	{
		IQueryable<T> query = PrepareQuery(
			x => x.Id.Equals(id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return query.SingleOrDefault();
	}

	public async Task<T?> GetByIdAsync(int id, bool ignoreQueryFilters = false, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		IQueryable<T> query = PrepareQuery(
				x => x.Id.Equals(id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
				);

		return await query.SingleOrDefaultAsync(cancellationToken);
	}

	public IEnumerable<T> GetByIds(IEnumerable<int> ids, bool ignoreQueryFilters = false, bool trackChanges = false)
	{
		IQueryable<T> query = PrepareQuery(
			x => ids.Contains(x.Id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return [.. query];
	}

	public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids, bool ignoreQueryFilters = false, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		IQueryable<T> query =
			PrepareQuery(x => ids.Contains(x.Id), ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges);

		return await query.ToListAsync(cancellationToken);
	}

	public T? GetByName(string name, bool ignoreQueryFilters = false, bool trackChanges = false)
	{
		IQueryable<T> query = PrepareQuery(
			x => x.Name == name, ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return query.SingleOrDefault();
	}

	public async Task<T?> GetByNameAsync(string name, bool ignoreQueryFilters = false,
		bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		IQueryable<T> query = PrepareQuery(
			x => x.Name == name, ignoreQueryFilters: ignoreQueryFilters, trackChanges: trackChanges
			);

		return await query.SingleOrDefaultAsync(cancellationToken);
	}
}
