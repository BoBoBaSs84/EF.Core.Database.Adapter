using DA.Base.Tests.Helpers;
using DA.Models.Contexts.Authentication;
using DA.Models.Contexts.Finances;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace DA.Repositories.Tests.Contexts.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class TransactionRepositoryTests : RepositoriesBaseTest
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

		IEnumerable<Transaction> dbTransactions =
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

		IEnumerable<Transaction> dbTransactions =
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

		IEnumerable<Transaction> dbTransactions =
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

		IEnumerable<Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetCardTransactionAsync(userId, cardNumber);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}
}