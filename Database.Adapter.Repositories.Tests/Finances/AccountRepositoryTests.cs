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
	public async Task GetAccountByIBANTest()
	{
		string iban = RandomHelper.GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(iban);
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		await repositoryManager.UserRepository.CreateAsync(newUser);
		await repositoryManager.CommitChangesAsync();

		Account dbaccount = await repositoryManager.AccountRepository.GetAccountAsync(iban);
		dbaccount.Should().NotBeNull();
	}

	[TestMethod]
	public async Task GetGetAccountsByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await repositoryManager.UserRepository.CreateAsync(newUser);
		await repositoryManager.CommitChangesAsync();

		IEnumerable<Account> dbAccounts = await repositoryManager.AccountRepository.GetAccountsAsync(newUser.Id);
		dbAccounts.Should().NotBeNullOrEmpty();
		dbAccounts.Should().HaveCount(newUser.AccountUsers.Count);
	}
}