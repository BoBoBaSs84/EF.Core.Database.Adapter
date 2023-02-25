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

namespace DA.RepositoriesTests.Contexts.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class TransactionRepositoryTests : RepositoriesBaseTest
{
	private readonly IUserService _userService = GetRequiredService<IUserService>();

	[TestMethod, Owner(Bobo)]
	public async Task GetAccountTransactionByUserIdAccountIdTest()
	{
		User newUser = GetNewUser(accountSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		int userId = newUser.Id,
			accountId = newUser.AccountUsers.Select(x => x.AccountId).FirstOrDefault();

		IEnumerable<Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetAccountTransactionAsync(userId, accountId);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAccountTransactionByUserIdAccountNumberTest()
	{
		User newUser = GetNewUser(accountSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		int userId = newUser.Id;
		string accountNumber = newUser.AccountUsers.Select(x => x.Account.IBAN).FirstOrDefault()!;

		IEnumerable<Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetAccountTransactionAsync(userId, accountNumber);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetCardTransactionByUserIdCardIdTest()
	{
		User newUser = GetNewUser(accountSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		int userId = newUser.Id,
			cardId = newUser.Cards.Select(x => x.Id).FirstOrDefault();

		IEnumerable<Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetCardTransactionAsync(userId, cardId);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetCardTransactionByUserIdCardNumberTest()
	{
		User newUser = GetNewUser(accountSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		int userId = newUser.Id;
		string cardNumber = newUser.Cards.Select(x => x.PAN).FirstOrDefault()!;

		IEnumerable<Transaction> dbTransactions =
			await RepositoryManager.TransactionRepository.GetCardTransactionAsync(userId, cardNumber);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}
}