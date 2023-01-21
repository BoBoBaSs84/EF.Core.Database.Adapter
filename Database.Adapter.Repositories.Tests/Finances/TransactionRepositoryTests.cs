using Database.Adapter.Base.Tests;
using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.Authentication;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Tests.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class TransactionRepositoryTests : BaseTest
{
	[TestMethod]
	public async Task GetAccountTransactionByUserIdAccountIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.UserRepository.CreateAsync(EntityHelper.GetNewUser(accountSeed: true));
		await RepositoryManager.CommitChangesAsync();
		int userId = newUser.Id,
			accountId = newUser.AccountUsers.Select(x => x.AccountId).FirstOrDefault();

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetAccountTransactionAsync(userId, accountId);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}

	[TestMethod]
	public async Task GetAccountTransactionByUserIdAccountNumberTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.UserRepository.CreateAsync(EntityHelper.GetNewUser(accountSeed: true));
		await RepositoryManager.CommitChangesAsync();
		int userId = newUser.Id;
		string accountNumber = newUser.AccountUsers.Select(x => x.Account.IBAN).FirstOrDefault()!;

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetAccountTransactionAsync(userId, accountNumber);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}

	[TestMethod]
	public async Task GetCardTransactionByUserIdCardIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.UserRepository.CreateAsync(EntityHelper.GetNewUser(accountSeed: true));
		await RepositoryManager.CommitChangesAsync();
		int userId = newUser.Id,
			cardId = newUser.Cards.Select(x => x.Id).FirstOrDefault();

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetCardTransactionAsync(userId, cardId);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}

	[TestMethod]
	public async Task GetCardTransactionByUserIdCardNumberTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.UserRepository.CreateAsync(EntityHelper.GetNewUser(accountSeed: true));
		await RepositoryManager.CommitChangesAsync();
		int userId = newUser.Id;
		string cardNumber = newUser.Cards.Select(x => x.PAN).FirstOrDefault()!;

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetCardTransactionAsync(userId, cardNumber);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}
}