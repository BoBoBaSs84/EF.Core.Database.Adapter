using Database.Adapter.Repositories.Contexts.Finances;
using Database.Adapter.Repositories.Contexts.Finances.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private Lazy<IAccountRepository> lazyAccountRepository = default!;
	private Lazy<ICardRepository> lazyCardRepository = default!;
	private Lazy<ITransactionRepository> lazyTransactionRepository = default!;

	/// <inheritdoc/>
	public IAccountRepository AccountRepository => lazyAccountRepository.Value;
	/// <inheritdoc/>
	public ICardRepository CardRepository => lazyCardRepository.Value;
	/// <inheritdoc/>
	public ITransactionRepository TransactionRepository => lazyTransactionRepository.Value;

	private void InitializeFinances()
	{
		lazyAccountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(DbContext));
		lazyCardRepository = new Lazy<ICardRepository>(() => new CardRepository(DbContext));
		lazyTransactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(DbContext));
	}
}
