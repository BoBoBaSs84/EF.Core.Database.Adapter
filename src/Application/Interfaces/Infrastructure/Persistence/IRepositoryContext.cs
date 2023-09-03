using Domain.Models.Attendance;
using Domain.Models.Common;
using Domain.Models.Finance;

using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Infrastructure.Persistence;

/// <summary>
/// The application context interface.
/// </summary>
public interface IRepositoryContext
{
	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Account"/>.
	/// </summary>
	DbSet<Account> Accounts { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="AttendanceModel"/>.
	/// </summary>
	DbSet<AttendanceModel> Attendances { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="CalendarDay"/>.
	/// </summary>
	DbSet<CalendarDay> CalendarDays { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Card"/>.
	/// </summary>
	DbSet<Card> Cards { get; }

	/// <summary>
	/// The <see cref="DbSet{TEntity}"/> of type <see cref="Transaction"/>.
	/// </summary>
	DbSet<Transaction> Transactions { get; }

	/// <summary>
	/// Saves the changes asynchronous.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}