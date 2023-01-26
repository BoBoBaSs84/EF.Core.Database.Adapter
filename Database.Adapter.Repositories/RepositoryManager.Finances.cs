using Database.Adapter.Repositories.Contexts.Finances;
using Database.Adapter.Repositories.Contexts.Finances.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<IAccountRepository> lazyAccountRepository = default!;
	private readonly Lazy<ICardRepository> lazyCardRepository = default!;
	private readonly Lazy<ITransactionRepository> lazyTransactionRepository = default!;

	/// <inheritdoc/>
	public IAccountRepository AccountRepository =>
		lazyAccountRepository.Value ?? new Lazy<IAccountRepository>(() => new AccountRepository(DbContext)).Value;
	/// <inheritdoc/>
	public ICardRepository CardRepository =>
		lazyCardRepository.Value ?? new Lazy<ICardRepository>(() => new CardRepository(DbContext)).Value;
	/// <inheritdoc/>
	public ITransactionRepository TransactionRepository =>
		lazyTransactionRepository.Value ?? new Lazy<ITransactionRepository>(() => new TransactionRepository(DbContext)).Value;
}
