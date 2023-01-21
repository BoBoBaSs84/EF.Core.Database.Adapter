﻿using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Entities.Extensions;
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

	public async Task<IEnumerable<Transaction>> GetAccountTransactionAsync(int userId, int accountId,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.AccountTransactions.SelectMany(x => x.Account.AccountUsers)
			.Any(x => x.UserId.Equals(userId)) && x.AccountTransactions
			.Any(x => x.AccountId.Equals(accountId)),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<Transaction>> GetAccountTransactionAsync(int userId, string iban,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.AccountTransactions.SelectMany(x => x.Account.AccountUsers)
			.Any(x => x.UserId.Equals(userId)) && x.AccountTransactions.Select(x => x.Account)
			.Any(x => x.IBAN.Equals(iban.RemoveWhitespace())),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<Transaction>> GetCardTransactionAsync(int userId, int cardId,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.CardTransactions.Select(x => x.Card)
			.Any(x => x.UserId.Equals(userId) && x.Id.Equals(cardId)),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<Transaction>> GetCardTransactionAsync(int userId, string pan,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.CardTransactions.Select(x => x.Card)
			.Any(x => x.UserId.Equals(userId) && x.PAN.Equals(pan.RemoveWhitespace())),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
