using Application.Interfaces.Infrastructure;
using Application.Interfaces.Infrastructure.Identity;
using BaseTests.Helpers;
using Domain;
using Domain.Entities.Finance;
using Domain.Entities.Identity;
using Domain.Extensions;
using FluentAssertions;
using InfrastructureTests.Helpers;
using Microsoft.AspNetCore.Identity;
using static BaseTests.Constants;

namespace InfrastructureTests.Persistence.Repositories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class AccountRepositoryTests : InfrastructureBaseTests
{
	private IUnitOfWork _unitOfWork = default!;
	private IUserService _userService = default!;

	[TestMethod, Owner(Bobo)]
	public async Task GetAccountByIBANTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		string iban = RandomHelper.GetString(Constants.Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(iban);
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		string password = RandomHelper.GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Account dbaccount = await _unitOfWork.AccountRepository.GetAccountAsync(iban);

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbaccount.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetGetAccountsByUserIdTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		IEnumerable<Account> dbAccounts = await _unitOfWork.AccountRepository.GetAccountsAsync(newUser.Id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAccounts.Should().NotBeNullOrEmpty();
			dbAccounts.Should().HaveCount(newUser.AccountUsers.Count);
		});
	}
}
