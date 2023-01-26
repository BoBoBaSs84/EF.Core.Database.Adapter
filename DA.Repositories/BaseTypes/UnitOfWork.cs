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
public abstract class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
{
	/// <summary>
	/// The standard constructor.
	/// </summary>
	public UnitOfWork() =>
		Context = new TContext();

	/// <summary>
	/// The <see cref="Context"/> property.
	/// </summary>
	protected TContext Context { get; }

	/// <inheritdoc/>
	public int CommitChanges() =>
		Context.SaveChanges();
	/// <inheritdoc/>
	public Task<int> CommitChangesAsync(CancellationToken cancellationToken = default) =>
		Context.SaveChangesAsync(cancellationToken);
}
