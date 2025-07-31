using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Tests;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class CardServiceTests : ApplicationTestBase
{
	[TestMethod]
	public async Task UpdateAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(id, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.UpdateFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task UpdateAsyncShouldReturnNotFoundWhenAccountNotFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, default, true, token))
			.Returns(Task.FromResult<CardEntity?>(null));
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(id, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.UpdateNotFound(id));
			cardMock.Verify(x => x.GetByIdAsync(id, default, true, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task UpdateAsyncShouldReturnUpdatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardEntity card = new();
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, default, true, token))
			.Returns(Task.FromResult<CardEntity?>(card));
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(id, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			card.Type.Should().Be(request.Type);
			card.ValidUntil.Should().Be(request.ValidUntil);
			cardMock.Verify(x => x.GetByIdAsync(id, default, true, token), Times.Once);
			cardMock.Verify(x => x.UpdateAsync(card, token), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
