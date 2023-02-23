﻿using DA.BaseTests.Helpers;
using DA.Models.Contexts.Authentication;
using DA.Models.Contexts.Finances;
using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;
using static DA.Models.Constants;

namespace DA.RepositoriesTests.Contexts.Finances;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CardRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public async Task GetCardByNumberTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string newIBAN = RandomHelper.GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(newIBAN);
		string newCardNumber = RandomHelper.GetString(Regex.CC).RemoveWhitespace();
		Card newCard = EntityHelper.GetNewCard(newUser, newAccount, newCardNumber);
		newAccount.Cards.Add(newCard);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		Card dbCard = await RepositoryManager.CardRepository.GetCardAsync(newCardNumber);
		dbCard.Should().NotBeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetCardsByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		IEnumerable<Card> dbCards = await RepositoryManager.CardRepository.GetCardsAsync(newUser.Id);

		AssertInScope(() =>
		{
			dbCards.Should().NotBeNullOrEmpty();
			dbCards.Should().HaveCount(newUser.Cards.Count);
		});
	}
}