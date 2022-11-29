using Database.Adapter.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Infrastructure;

/// <summary>
/// The unit of work class.
/// </summary>
/// <remarks>
/// Implemnts the members of the <see cref="IUnitOfWork{TContext}"/> interface.
/// </remarks>
/// <typeparam name="TContext"></typeparam>
public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
{
	/// <summary>
	/// The current database context.
	/// </summary>
	protected readonly DbContext dbContext;
	/// <summary>
	/// The standard constructor.
	/// </summary>
	public UnitOfWork() =>
		dbContext = new TContext();
	/// <inheritdoc/>
	public int CommitChanges() =>
		dbContext.SaveChanges();
	/// <inheritdoc/>
	public Task<int> CommitChangesAsync(CancellationToken cancellationToken = default) =>
		dbContext.SaveChangesAsync(cancellationToken);
}
