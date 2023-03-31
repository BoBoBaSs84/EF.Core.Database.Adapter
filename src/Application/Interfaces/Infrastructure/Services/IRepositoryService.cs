using Application.Interfaces.Infrastructure.Persistence.Repositories;

namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The "Unit of Work" interface.
/// </summary>
public interface IRepositoryService
{
	/// <summary>
	/// The <see cref="AccountRepository"/> interface.
	/// </summary>
	IAccountRepository AccountRepository { get; }

	/// <summary>
	/// The <see cref="AttendanceRepository"/> interface.
	/// </summary>
	IAttendanceRepository AttendanceRepository { get; }

	/// <summary>
	/// The <see cref="CalendarDayRepository"/> interface.
	/// </summary>
	ICalendarDayRepository CalendarDayRepository { get; }

	/// <summary>
	/// The <see cref="CardRepository"/> interface.
	/// </summary>
	ICardRepository CardRepository { get; }

	/// <summary>
	/// The <see cref="CardTypeRepository"/> interface.
	/// </summary>
	ICardTypeRepository CardTypeRepository { get; }

	/// <summary>
	/// The <see cref="DayTypeRepository"/> interface.
	/// </summary>
	IDayTypeRepository DayTypeRepository { get; }

	/// <summary>
	/// The <see cref="TransactionRepository"/> interface.
	/// </summary>
	ITransactionRepository TransactionRepository { get; }

	/// <summary>
	/// Should commit all the changes to the database context async.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>From the commit affected changes.</returns>
	Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
}
