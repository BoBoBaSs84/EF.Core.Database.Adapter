namespace Application.Interfaces.Infrastructure.Repositories.BaseTypes;

/// <summary>
/// The enumerator repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEntity">The enumerator entity to work with.</typeparam>
public interface IEnumeratorRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
	/// <summary>
	/// Should return only active <typeparamref name="TEntity"/> entities.
	/// </summary>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of active enumerator entities.</returns>
	Task<IEnumerable<TEntity>> GetAllActiveAsync(bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the <typeparamref name="TEntity"/> entity by its unique name.
	/// </summary>
	/// <param name="name">The name of the enumerator.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The named enumerator entity.</returns>
	Task<TEntity> GetByNameAsync(string name, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return a collection of <typeparamref name="TEntity"/> entities by their unique names.
	/// </summary>
	/// <param name="names">The names of the enumerators.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of named enumerator entities.</returns>/// <returns></returns>
	Task<IEnumerable<TEntity>> GetByNamesAsync(IEnumerable<string> names, bool trackChanges = false, CancellationToken cancellationToken = default);
}
