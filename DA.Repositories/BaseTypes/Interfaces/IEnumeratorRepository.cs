namespace DA.Repositories.BaseTypes.Interfaces;

/// <summary>
/// The enumerator repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEnumeratorEntity">The enumerator entity.</typeparam>
public interface IEnumeratorRepository<TEnumeratorEntity> : IGenericRepository<TEnumeratorEntity> where TEnumeratorEntity : class
{
	/// <summary>
	/// Should return only active enumerator entities.
	/// </summary>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>All active enumerator entities.</returns>
	Task<IEnumerable<TEnumeratorEntity>> GetAllActiveAsync(bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should return the enumerator by its unique name.
	/// </summary>
	/// <param name="name">The name of the enumerator.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>The named enumerator.</returns>
	Task<TEnumeratorEntity> GetByNameAsync(string name, bool trackChanges = false, CancellationToken cancellationToken = default);
}
