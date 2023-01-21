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
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
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
	public void GetCardByNumberTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		string newIBAN = RandomHelper.GetString(Regex.IBAN).RemoveWhitespace();
		Account newAccount = EntityHelper.GetNewAccount(newIBAN);
		string newCardNumber = RandomHelper.GetString(Regex.CC).RemoveWhitespace();
		Card newCard = EntityHelper.GetNewCard(newUser, newAccount, newCardNumber);
		newAccount.Cards.Add(newCard);
		newUser.AccountUsers.Add(new() { Account = newAccount, User = newUser });
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();

		Card dbCard = repositoryManager.CardRepository.GetCard(newCardNumber);
		dbCard.Should().NotBeNull();
	}

	[TestMethod]
	public void GetCardsByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(accountSeed: true);
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();
		int dbUserId = repositoryManager.UserRepository.GetByCondition(x => x.UserName.Equals(newUser.UserName)).Id;

		IEnumerable<Card> dbCards = repositoryManager.CardRepository.GetCards(dbUserId);
		dbCards.Should().NotBeNullOrEmpty();
		dbCards.Should().HaveCount(newUser.Cards.Count);
	}
}