using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CardTypeRepositoryTests
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
	public async Task GetByNameFailedTest()
	{
		string cardTypeName = RandomHelper.GetString(12);

		CardType cardType = await repositoryManager.CardTypeRepository.GetByNameAsync(cardTypeName);

		cardType.Should().BeNull();
	}

	[TestMethod]
	public async Task GetByNameSuccessTest()
	{
		string cardTypeName = Entities.Enumerators.CardType.CREDIT.GetName();

		CardType cardType = await repositoryManager.CardTypeRepository.GetByNameAsync(cardTypeName);

		cardType.Should().NotBeNull();
		cardType.Name.Should().Be(cardTypeName);
	}

	[TestMethod]
	public async Task GetAllActiveTest()
	{
		IEnumerable<CardType> cardTypes = await repositoryManager.CardTypeRepository.GetAllActiveAsync();
		cardTypes.Should().NotBeNullOrEmpty();
	}
}
