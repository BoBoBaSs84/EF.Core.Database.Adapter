using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

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
	/// The card repository interface.
	/// </summary>
	ICardRepository CardRepository { get; }

	/// <summary>
	/// The transaction repository interface.
	/// </summary>
	ITransactionRepository TransactionRepository { get; }

	/// <summary>
	/// The todo list repository interface.
	/// </summary>
	IListRepository TodoListRepository { get; }

	/// <summary>
	/// The todo item repository interface.
	/// </summary>
	IItemRepository TodoItemRepository { get; }

	/// <summary>
	/// Should commit all the changes to the database context async.
	/// </summary>
	/// <param name="token">The cancellation token.</param>
	/// <returns>From the commit affected changes.</returns>
	Task<int> CommitChangesAsync(CancellationToken token = default);
}
