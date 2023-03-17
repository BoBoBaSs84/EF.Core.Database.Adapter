using Application.Interfaces.Infrastructure;
using Application.Interfaces.Infrastructure.Identity;
using BaseTests.Helpers;
using Domain.Constants;
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
public sealed class CardRepositoryTests : InfrastructureBaseTests
{
	private IUnitOfWork _unitOfWork = default!;
	private IUserService _userService = default!;

	[TestMethod, Owner(Bobo)]
	public async Task GetCardByNumberTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string newIBAN = RandomHelper.GetString(DomainConstants.Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(newIBAN);
		string newCardNumber = RandomHelper.GetString(DomainConstants.Regex.CC).RemoveWhitespace();
		Card newCard = EntityHelper.GetNewCard(newUser, newAccount, newCardNumber);
		newAccount.Cards.Add(newCard);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		string password = RandomHelper.GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Card dbCard = await _unitOfWork.CardRepository.GetCardAsync(newCardNumber);

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbCard.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetCardsByUserIdTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		IEnumerable<Card> dbCards = await _unitOfWork.CardRepository.GetCardsAsync(newUser.Id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbCards.Should().NotBeNullOrEmpty();
			dbCards.Should().HaveCount(newUser.Cards.Count);
		});
	}
}
