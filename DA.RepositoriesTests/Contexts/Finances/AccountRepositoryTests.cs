using DA.Domain.Extensions;
using DA.Domain.Models.Finances;
using DA.Domain.Models.Identity;
using DA.Infrastructure.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;
using static DA.BaseTests.Helpers.EntityHelper;
using static DA.BaseTests.Helpers.RandomHelper;
using static DA.Domain.Constants;

namespace DA.RepositoriesTests.Contexts.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AccountRepositoryTests : RepositoriesBaseTest
{
	private readonly IUserService _userService = GetRequiredService<IUserService>();

	[TestMethod, Owner(Bobo)]
	public async Task GetAccountByIBANTest()
	{
		string iban = GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = GetNewAccount(iban);
		User newUser = GetNewUser(accountSeed: true);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Account dbaccount = await RepositoryManager.AccountRepository.GetAccountAsync(iban);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbaccount.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetGetAccountsByUserIdTest()
	{
		User newUser = GetNewUser(accountSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		IEnumerable<Account> dbAccounts = await RepositoryManager.AccountRepository.GetAccountsAsync(newUser.Id);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAccounts.Should().NotBeNullOrEmpty();
			dbAccounts.Should().HaveCount(newUser.AccountUsers.Count);
		});
	}
}