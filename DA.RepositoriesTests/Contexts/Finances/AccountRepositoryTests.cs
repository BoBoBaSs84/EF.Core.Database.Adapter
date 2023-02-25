using DA.BaseTests.Helpers;
using DA.Domain.Extensions;
using DA.Domain.Models.Finances;
using DA.Domain.Models.Identity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;
using static DA.Domain.Constants;

namespace DA.RepositoriesTests.Contexts.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AccountRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
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

	[TestMethod, Owner(Bobo)]
	public async Task GetGetAccountsByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		IEnumerable<Account> dbAccounts = await RepositoryManager.AccountRepository.GetAccountsAsync(newUser.Id);

		AssertInScope(() =>
		{
			dbAccounts.Should().NotBeNullOrEmpty();
			dbAccounts.Should().HaveCount(newUser.AccountUsers.Count);
		});
	}
}