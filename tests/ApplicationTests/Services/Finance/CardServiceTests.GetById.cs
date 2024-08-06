using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

using Domain.Enumerators.Finance;
using Domain.Errors;
using Domain.Models.Finance;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class CardServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(CardService.GetById))]
	public async Task GetByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CardService sut = CreateMockedInstance();

		ErrorOr<CardResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.GetByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.GetById))]
	public async Task GetByIdShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<CardModel?>(null));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<CardResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.GetByIdNotFound(id));
			cardMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.GetById))]
	public async Task GetByIdShouldReturnResponseWithNoCardsWhenCardsNotFound()
	{
		Guid id = Guid.NewGuid();
		CardModel cardModel = new() { Id = id, Type = CardType.DEBIT, PAN = "UnitTest", ValidUntil = DateTime.Today };
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, false, false, default))
			.Returns(Task.FromResult<CardModel?>(cardModel));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<CardResponse> result = await sut.GetById(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Id.Should().Be(cardModel.Id);
			result.Value.Type.Should().Be(cardModel.Type);
			result.Value.PAN.Should().Be(cardModel.PAN);
			result.Value.ValidUntil.Should().Be(cardModel.ValidUntil);
			cardMock.Verify(x => x.GetByIdAsync(id, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
