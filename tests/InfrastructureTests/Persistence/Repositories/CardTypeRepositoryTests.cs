using Application.Common.Interfaces;
using BaseTests.Helpers;
using Domain.Entities.Enumerator;
using Domain.Enumerators;
using Domain.Extensions;
using FluentAssertions;
using static BaseTests.Constants;

namespace InfrastructureTests.Persistence.Repositories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class CardTypeRepositoryTests : InfrastructureBaseTests
{
	private IUnitOfWork _unitOfWork = default!;

	[TestMethod, Owner(Bobo)]
	public async Task GetByNameAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		string cardTypeName = CardTypes.CREDIT.GetName();
		CardType cardType = await _unitOfWork.CardTypeRepository.GetByNameAsync(cardTypeName);

		AssertionHelper.AssertInScope(() =>
		{
			cardType.Should().NotBeNull();
			cardType.Name.Should().Be(cardTypeName);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNamesAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<string> cardNames = new[] { CardTypes.CREDIT.GetName(), CardTypes.DEBIT.GetName() };
		IEnumerable<CardType> cardTypes = await _unitOfWork.CardTypeRepository.GetByNamesAsync(cardNames);

		AssertionHelper.AssertInScope(() =>
		{
			cardTypes.Should().NotBeNullOrEmpty();
			cardTypes.Should().HaveCount(cardNames.Count());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAllActiveAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<CardType> cardTypes = await _unitOfWork.CardTypeRepository.GetAllActiveAsync();

		cardTypes.Should().NotBeNullOrEmpty();
	}
}
