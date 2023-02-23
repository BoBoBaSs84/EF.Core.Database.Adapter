using DA.BaseTests.Helpers;
using DA.Models.Contexts.Authentication;
using DA.Models.Contexts.Finances;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;

namespace DA.RepositoriesTests.Contexts.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class TransactionRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
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

		AssertInScope(() =>
		{
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}

	[TestMethod, Owner(Bobo)]
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

		AssertInScope(() =>
		{
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}

	[TestMethod, Owner(Bobo)]
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

		AssertInScope(() =>
		{
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}

	[TestMethod, Owner(Bobo)]
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

		AssertInScope(() =>
		{
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}
}