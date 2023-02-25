using DA.Repositories.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.BaseTypes;

/// <summary>
/// The unit of work class.
/// </summary>
/// <remarks>
/// Implemnts the members of the <see cref="IUnitOfWork{TContext}"/> interface.
/// </remarks>
/// <typeparam name="TContext"></typeparam>
public abstract class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
	/// </summary>
	/// <param name="context">The database context.</param>
	public UnitOfWork(TContext context) => DbContext = context;

	/// <summary>
	/// The <see cref="DbContext"/> property.
	/// </summary>
	protected TContext DbContext { get; }

	/// <inheritdoc/>
	public int CommitChanges() => DbContext.SaveChanges();

	/// <inheritdoc/>
	public Task<int> CommitChangesAsync(CancellationToken cancellationToken = default) =>
		DbContext.SaveChangesAsync(cancellationToken);
}
