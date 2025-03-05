using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;

namespace BB84.Home.Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The repository service interface.
/// </summary>
public interface IRepositoryService
{
	/// <summary>
	/// The account repository instance.
	/// </summary>
	IAccountRepository AccountRepository { get; }

	/// <summary>
	/// The attendance repository instance.
	/// </summary>
	IAttendanceRepository AttendanceRepository { get; }

	/// <summary>
	/// The card repository instance.
	/// </summary>
	ICardRepository CardRepository { get; }

	/// <summary>
	/// The document repository instance.
	/// </summary>
	IDocumentRepository DocumentRepository { get; }

	/// <summary>
	///  The document data repository instance.
	/// </summary>
	IDocumentDataRepository DocumentDataRepository { get; }

	/// <summary>
	///  The document extension repository instance.
	/// </summary>
	IDocumentExtensionRepository DocumentExtensionRepository { get; }

	/// <summary>
	/// The transaction repository instance.
	/// </summary>
	ITransactionRepository TransactionRepository { get; }

	/// <summary>
	/// The todo list repository instance.
	/// </summary>
	IListRepository TodoListRepository { get; }

	/// <summary>
	/// The todo item repository instance.
	/// </summary>
	IItemRepository TodoItemRepository { get; }

	/// <summary>
	/// Should commit all the changes to the database context async.
	/// </summary>
	/// <param name="token">The cancellation token.</param>
	/// <returns>From the commit affected changes.</returns>
	Task<int> CommitChangesAsync(CancellationToken token = default);
}
