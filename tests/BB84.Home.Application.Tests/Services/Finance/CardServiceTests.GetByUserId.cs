using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Finance;
using BB84.Home.Application.Tests;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class CardServiceTests : ApplicationTestBase
{
	[TestMethod]
	public async Task GetAllAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<CardResponse>> result = await _sut
			.GetAllAsync(token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.GetByUserIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task GetAllAsyncShouldReturnResponseWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		IEnumerable<CardEntity> cards = [new(), new(), new()];
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetAllAsync(false, false, token))
			.Returns(Task.FromResult(cards));
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<IEnumerable<CardResponse>> result = await _sut
			.GetAllAsync(token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Count().Should().Be(cards.Count());
			cardMock.Verify(x => x.GetAllAsync(false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
