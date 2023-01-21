using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Contexts.Finances.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Contexts.Finances;

/// <summary>
/// The transaction repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAccountRepository"/> interface</item>
/// </list>
/// </remarks>
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TransactionRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public TransactionRepository(DbContext dbContext) : base(dbContext)
	{
	}

	/// <inheritdoc/>
	public IEnumerable<Transaction> GetAccountTransaction(int userId, int accountId, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.AccountTransactions.SelectMany(x => x.Account.AccountUsers)
			.Any(x => x.UserId.Equals(userId)) && x.AccountTransactions
			.Any(x => x.AccountId.Equals(accountId)),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public IEnumerable<Transaction> GetAccountTransaction(int userId, string accountNumber, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.AccountTransactions.SelectMany(x => x.Account.AccountUsers)
			.Any(x => x.UserId.Equals(userId)) && x.AccountTransactions.Select(x => x.Account)
			.Any(x => x.IBAN.Equals(accountNumber)),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public IEnumerable<Transaction> GetCardTransaction(int userId, int cardId, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.CardTransactions.Select(x => x.Card)
			.Any(x => x.UserId.Equals(userId) && x.Id.Equals(cardId)),
			trackChanges: trackChanges
			);
	/// <inheritdoc/>
	public IEnumerable<Transaction> GetCardTransaction(int userId, string cardNumber, bool trackChanges = false) =>
		GetManyByCondition(
			expression: x => x.CardTransactions.Select(x => x.Card)
			.Any(x => x.UserId.Equals(userId) && x.Number.Equals(cardNumber)),
			trackChanges: trackChanges
			);
}
