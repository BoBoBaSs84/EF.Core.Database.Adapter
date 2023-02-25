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
public class CardRepositoryTests : RepositoriesBaseTest
{
	private readonly IUserService _userService = GetRequiredService<IUserService>();

	[TestMethod, Owner(Bobo)]
	public async Task GetCardByNumberTest()
	{
		User newUser = GetNewUser(accountSeed: true);
		string newIBAN = GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = GetNewAccount(newIBAN);
		string newCardNumber = GetString(Regex.CC).RemoveWhitespace();
		Card newCard = GetNewCard(newUser, newAccount, newCardNumber);
		newAccount.Cards.Add(newCard);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Card dbCard = await RepositoryManager.CardRepository.GetCardAsync(newCardNumber);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbCard.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetCardsByUserIdTest()
	{
		User newUser = GetNewUser(accountSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		IEnumerable<Card> dbCards = await RepositoryManager.CardRepository.GetCardsAsync(newUser.Id);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbCards.Should().NotBeNullOrEmpty();
			dbCards.Should().HaveCount(newUser.Cards.Count);
		});
	}
}