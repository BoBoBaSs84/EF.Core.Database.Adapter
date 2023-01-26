using DA.Repositories.Contexts.Finances.Interfaces;

namespace Database.Adapter.Repositories.Interfaces;

public partial interface IRepositoryManager
{
	/// <summary>
	/// The <see cref="AccountRepository"/> interface.
	/// </summary>
	IAccountRepository AccountRepository { get; }
	/// <summary>
	/// The <see cref="CardRepository"/> interface.
	/// </summary>
	ICardRepository CardRepository { get; }
	/// <summary>
	/// The <see cref="TransactionRepository"/> interface.
	/// </summary>
	ITransactionRepository TransactionRepository { get; }
}
