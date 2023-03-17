using Application.Interfaces.Infrastructure;
using Application.Interfaces.Infrastructure.Identity;
using BaseTests.Helpers;
using Domain.Entities.Finance;
using Domain.Entities.Identity;
using FluentAssertions;
using InfrastructureTests.Helpers;
using Microsoft.AspNetCore.Identity;
using static BaseTests.Constants.TestConstants;

namespace InfrastructureTests.Persistence.Repositories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class TransactionRepositoryTests : InfrastructureBaseTests
{
	private IUnitOfWork _unitOfWork = default!;
	private IUserService _userService = default!;

	[TestMethod, Owner(Bobo)]
	public async Task GetAccountTransactionByUserIdAccountIdTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);
		IdentityResult result = await _userService.CreateAsync(newUser, password);
		int accountId = newUser.AccountUsers.Select(x => x.AccountId).FirstOrDefault();

		IEnumerable<Transaction> dbTransactions =
			await _unitOfWork.TransactionRepository.GetAccountTransactionAsync(newUser.Id, accountId);

		AssertionHelper.AssertInScope(() =>
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
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);
		IdentityResult result = await _userService.CreateAsync(newUser, password);
		string accountNumber = newUser.AccountUsers.Select(x => x.Account.IBAN).FirstOrDefault()!;

		IEnumerable<Transaction> dbTransactions =
			await _unitOfWork.TransactionRepository.GetAccountTransactionAsync(newUser.Id, accountNumber);

		AssertionHelper.AssertInScope(() =>
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
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);
		IdentityResult result = await _userService.CreateAsync(newUser, password);
		int cardId = newUser.Cards.Select(x => x.Id).FirstOrDefault();

		IEnumerable<Transaction> dbTransactions =
			await _unitOfWork.TransactionRepository.GetCardTransactionAsync(newUser.Id, cardId);

		AssertionHelper.AssertInScope(() =>
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
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);
		IdentityResult result = await _userService.CreateAsync(newUser, password);
		string cardNumber = newUser.Cards.Select(x => x.PAN).FirstOrDefault()!;

		IEnumerable<Transaction> dbTransactions =
			await _unitOfWork.TransactionRepository.GetCardTransactionAsync(newUser.Id, cardNumber);

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbTransactions.Should().NotBeNullOrEmpty();
			dbTransactions.Should().HaveCount(2);
		});
	}
}
