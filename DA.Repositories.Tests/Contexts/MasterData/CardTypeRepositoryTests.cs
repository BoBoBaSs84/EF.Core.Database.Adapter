﻿using DA.Base.Tests.Helpers;
using DA.Models.Contexts.MasterData;
using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.Base.Tests.Constants;

namespace DA.Repositories.Tests.Contexts.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CardTypeRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public async Task GetByNameFailedTest()
	{
		string cardTypeName = RandomHelper.GetString(12);

		CardType cardType = await RepositoryManager.CardTypeRepository.GetByNameAsync(cardTypeName);

		cardType.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNamesFailedTest()
	{
		IEnumerable<string> cardNames = new[]
		{
			RandomHelper.GetString(12),
			RandomHelper.GetString(12)
		};

		IEnumerable<CardType> cardTypes = await RepositoryManager.CardTypeRepository.GetByNamesAsync(cardNames);

		cardTypes.Should().BeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNameSuccessTest()
	{
		string cardTypeName = Models.Enumerators.CardType.CREDIT.GetName();

		CardType cardType = await RepositoryManager.CardTypeRepository.GetByNameAsync(cardTypeName);

		cardType.Should().NotBeNull();
		cardType.Name.Should().Be(cardTypeName);
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNamesSuccessTest()
	{
		IEnumerable<string> cardNames = new[]
		{
			Models.Enumerators.CardType.CREDIT.GetName(),
			Models.Enumerators.CardType.DEBIT.GetName()
		};

		IEnumerable<CardType> cardTypes = await RepositoryManager.CardTypeRepository.GetByNamesAsync(cardNames);

		cardTypes.Should().NotBeNullOrEmpty();
		cardTypes.Should().HaveCount(cardNames.Count());
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAllActiveTest()
	{
		IEnumerable<CardType> cardTypes = await RepositoryManager.CardTypeRepository.GetAllActiveAsync();
		cardTypes.Should().NotBeNullOrEmpty();
	}
}