using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class TransactionRepositoryTests
{
	private TransactionScope transactionScope = default!;
	private IRepositoryManager repositoryManager = default!;

	[TestInitialize]
	public void TestInitialize()
	{
		transactionScope = new TransactionScope();
		repositoryManager = new RepositoryManager();
	}

	[TestCleanup]
	public void TestCleanup() => transactionScope.Dispose();

	[TestMethod]
	public void GetAccountTransactionByUserIdAccountIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.UserRepository.Create(EntityHelper.GetNewUser(accountSeed: true));
		repositoryManager.CommitChanges();
		int userId = newUser.Id,
			accountId = newUser.AccountUsers.Select(x => x.AccountId).FirstOrDefault();

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
			repositoryManager.TransactionRepository.GetAccountTransaction(userId, accountId);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}

	[TestMethod]
	public void GetAccountTransactionByUserIdAccountNumberTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.UserRepository.Create(EntityHelper.GetNewUser(accountSeed: true));
		repositoryManager.CommitChanges();
		int userId = newUser.Id;
		string accountNumber = newUser.AccountUsers.Select(x => x.Account.IBAN).FirstOrDefault()!;

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
			repositoryManager.TransactionRepository.GetAccountTransaction(userId, accountNumber);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}

	[TestMethod]
	public void GetCardTransactionByUserIdCardIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.UserRepository.Create(EntityHelper.GetNewUser(accountSeed: true));
		repositoryManager.CommitChanges();
		int userId = newUser.Id,
			cardId = newUser.Cards.Select(x => x.Id).FirstOrDefault();

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
					repositoryManager.TransactionRepository.GetCardTransaction(userId, cardId);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}

	[TestMethod]
	public void GetCardTransactionByUserIdCardNumberTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.UserRepository.Create(EntityHelper.GetNewUser(accountSeed: true));
		repositoryManager.CommitChanges();
		int userId = newUser.Id;
		string cardNumber = newUser.Cards.Select(x => x.PAN).FirstOrDefault()!;

		IEnumerable<Entities.Contexts.Finances.Transaction> dbTransactions =
					repositoryManager.TransactionRepository.GetCardTransaction(userId, cardNumber);

		dbTransactions.Should().NotBeNullOrEmpty();
		dbTransactions.Should().HaveCount(2);
	}
}