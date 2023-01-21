using Database.Adapter.Base.Tests;
using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CardTypeRepositoryTests : BaseTest
{
	[TestMethod]
	public async Task GetByNameFailedTest()
	{
		string cardTypeName = RandomHelper.GetString(12);

		CardType cardType = await RepositoryManager.CardTypeRepository.GetByNameAsync(cardTypeName);

		cardType.Should().BeNull();
	}

	[TestMethod]
	public async Task GetByNameSuccessTest()
	{
		string cardTypeName = Entities.Enumerators.CardType.CREDIT.GetName();

		CardType cardType = await RepositoryManager.CardTypeRepository.GetByNameAsync(cardTypeName);

		cardType.Should().NotBeNull();
		cardType.Name.Should().Be(cardTypeName);
	}

	[TestMethod]
	public async Task GetAllActiveTest()
	{
		IEnumerable<CardType> cardTypes = await RepositoryManager.CardTypeRepository.GetAllActiveAsync();
		cardTypes.Should().NotBeNullOrEmpty();
	}
}
