using Application.Interfaces.Infrastructure.Persistence.Repositories;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The repository service interface.
/// </summary>
public interface IRepositoryService
{
	/// <summary>
	/// The account repository interface.
	/// </summary>
	IAccountRepository AccountRepository { get; }

	/// <summary>
	/// The attendance repository interface.
	/// </summary>
	IAttendanceRepository AttendanceRepository { get; }

	/// <summary>
	/// The calendar repository interface.
	/// </summary>
	ICalendarRepository CalendarRepository { get; }

	/// <summary>
	/// The card repository interface.
	/// </summary>
	ICardRepository CardRepository { get; }

	/// <summary>
	/// The transaction repository interface.
	/// </summary>
	ITransactionRepository TransactionRepository { get; }

	/// <summary>
	/// Should commit all the changes to the database context async.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>From the commit affected changes.</returns>
	Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
}
