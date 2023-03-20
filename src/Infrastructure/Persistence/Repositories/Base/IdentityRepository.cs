using Application.Interfaces.Infrastructure.Repositories.BaseTypes;
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

	public async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => ids.Contains(x.Id),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
