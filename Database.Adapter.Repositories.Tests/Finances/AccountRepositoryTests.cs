using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Entities.Contexts.Finances;
using Database.Adapter.Entities.Extensions;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using static Database.Adapter.Entities.Constants;

namespace Database.Adapter.Repositories.Tests.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
public class AccountRepositoryTests
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
	public void GetAccountByIBANTest()
	{
		string iban = RandomHelper.GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(iban);
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();			
		
		Account dbaccount = repositoryManager.AccountRepository.GetAccount(iban);
		dbaccount.Should().NotBeNull();
	}

	[TestMethod]
	public void GetGetAccountsByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();
		int dbUserId = repositoryManager.UserRepository.GetByCondition(x => x.UserName.Equals(newUser.UserName)).Id;

		IEnumerable<Account> dbAccounts = repositoryManager.AccountRepository.GetAccounts(dbUserId);
		dbAccounts.Should().NotBeNullOrEmpty();
		dbAccounts.Should().HaveCount(newUser.AccountUsers.Count);
	}
}