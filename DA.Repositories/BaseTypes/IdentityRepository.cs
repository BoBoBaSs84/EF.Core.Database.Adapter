using DA.Domain.Models.BaseTypes;
using DA.Repositories.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.BaseTypes;

/// <summary>
/// The identity repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TIdentityEntity">The identity entity.</typeparam>
internal abstract class IdentityRepository<TIdentityEntity> : GenericRepository<TIdentityEntity>, IIdentityRepository<TIdentityEntity> where TIdentityEntity : IdentityModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="IdentityRepository{TIdentityEntity}"/> class.
	/// </summary>
	/// <inheritdoc/>
	protected IdentityRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<IEnumerable<TIdentityEntity>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => ids.Contains(x.Id),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
