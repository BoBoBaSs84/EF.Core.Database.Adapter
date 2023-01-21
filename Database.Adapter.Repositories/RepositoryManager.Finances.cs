using Database.Adapter.Repositories.Contexts.Finances.Interfaces;

namespace Database.Adapter.Repositories;

public sealed partial class RepositoryManager
{
	private readonly Lazy<IAccountRepository> lazyAccountRepository;
	private readonly Lazy<ICardRepository> lazyCardRepository;
	private readonly Lazy<ITransactionRepository> lazyTransactionRepository;

	/// <inheritdoc/>
	public IAccountRepository AccountRepository => lazyAccountRepository.Value;
	/// <inheritdoc/>
	public ICardRepository CardRepository => lazyCardRepository.Value;
	/// <inheritdoc/>
	public ITransactionRepository TransactionRepository => lazyTransactionRepository.Value;
}
