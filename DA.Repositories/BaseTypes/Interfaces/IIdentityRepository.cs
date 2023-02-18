namespace DA.Repositories.BaseTypes.Interfaces;

/// <summary>
/// The identity repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TIdentityEntity">The identity entity.</typeparam>
public interface IIdentityRepository<TIdentityEntity> : IGenericRepository<TIdentityEntity> where TIdentityEntity : class
{
	/// <summary>
	/// Should fetch a collection of entites by their primary keys.
	/// </summary>
	/// <param name="ids">The primary key of the entities.</param>
	/// <param name="trackChanges">Should the fetched entities be tracked?</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A collection of entities.</returns>
	Task<IEnumerable<TIdentityEntity>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges = false, CancellationToken cancellationToken = default);
}
