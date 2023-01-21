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
public class CardRepositoryTests
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
	public async Task GetCardByNumberTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string newIBAN = RandomHelper.GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(newIBAN);
		string newCardNumber = RandomHelper.GetString(Regex.CC).RemoveWhitespace();
		Card newCard = EntityHelper.GetNewCard(newUser, newAccount, newCardNumber);
		newAccount.Cards.Add(newCard);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		await repositoryManager.UserRepository.CreateAsync(newUser);
		await repositoryManager.CommitChangesAsync();

		Card dbCard = await repositoryManager.CardRepository.GetCardAsync(newCardNumber);
		dbCard.Should().NotBeNull();
	}

	[TestMethod]
	public async Task GetCardsByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		await repositoryManager.UserRepository.CreateAsync(newUser);
		await repositoryManager.CommitChangesAsync();

		IEnumerable<Card> dbCards = await repositoryManager.CardRepository.GetCardsAsync(newUser.Id);
		dbCards.Should().NotBeNullOrEmpty();
		dbCards.Should().HaveCount(newUser.Cards.Count);
	}
}