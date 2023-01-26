using DA.Base.Tests.Helpers;
using DA.Models.Extensions;
using DA.Repositories.Tests;
using DA.Models.Contexts.Authentication;
using DA.Models.Contexts.Finances;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.Models.Constants;

namespace DA.Repositories.Tests.Contexts.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AccountRepositoryTests : RepositoriesBaseTest
{
	[TestMethod]
	public async Task GetAccountByIBANTest()
	{
		string iban = RandomHelper.GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(iban);
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		Account dbaccount = await RepositoryManager.AccountRepository.GetAccountAsync(iban);
		dbaccount.Should().NotBeNull();
	}

	[TestMethod]
	public async Task GetGetAccountsByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		IEnumerable<Account> dbAccounts = await RepositoryManager.AccountRepository.GetAccountsAsync(newUser.Id);
		dbAccounts.Should().NotBeNullOrEmpty();
		dbAccounts.Should().HaveCount(newUser.AccountUsers.Count);
	}
}