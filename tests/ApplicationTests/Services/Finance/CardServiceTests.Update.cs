using Application.Contracts.Requests.Finance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Finance;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class CardServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(CardService.Update))]
	public async Task UpdateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();
		CardService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.UpdateFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.Update))]
	public async Task UpdateShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid id = Guid.NewGuid();
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, default, true, default))
			.Returns(Task.FromResult<CardModel?>(null));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.UpdateNotFound(id));
			cardMock.Verify(x => x.GetByIdAsync(id, default, true, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.Update))]
	public async Task UpdateShouldReturnUpdatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CardModel card = new();
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, default, true, default))
			.Returns(Task.FromResult<CardModel?>(card));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(1));

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			card.Type.Should().Be(request.Type);
			card.ValidUntil.Should().Be(request.ValidUntil);
			cardMock.Verify(x => x.GetByIdAsync(id, default, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
