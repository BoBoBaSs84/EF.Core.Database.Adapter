using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.BaseTypes.Interfaces;

/// <summary>
/// The unit of work interface.
/// </summary>
/// <typeparam name="TContext">The database context.</typeparam>
public interface IUnitOfWork<TContext> where TContext : DbContext
{
	/// <summary>
	/// Should commit all the changes to the database context.
	/// </summary>
	/// <returns>Success code as integer.</returns>
	int CommitChanges();

	/// <summary>
	/// Should commit all the changes to the database context async.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>Success code as integer.</returns>
	Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
}
