using Domain.Entities.Enumerator;
using Domain.Entities.Finance;
using Domain.Entities.Private;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Infrastructure.Persistence;

/// <summary>
/// The application context interface.
/// </summary>
public interface IApplicationContext
{
	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Account"/>.
	/// </summary>
	DbSet<Account> Accounts { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Attendance"/>.
	/// </summary>
	DbSet<Attendance> Attendances { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CalendarDay"/>.
	/// </summary>
	DbSet<CalendarDay> CalendarDays { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Card"/>.
	/// </summary>
	DbSet<Card> Cards { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CardType"/>.
	/// </summary>
	DbSet<CardType> CardTypes { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="DayType"/>.
	/// </summary>
	DbSet<DayType> DayTypes { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Transaction"/>.
	/// </summary>
	DbSet<Transaction> Transactions { get; }

	/// <summary>
	/// Saves the changes.
	/// </summary>
	int SaveChanges();

	/// <summary>
	/// Saves the changes asynchronous.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}