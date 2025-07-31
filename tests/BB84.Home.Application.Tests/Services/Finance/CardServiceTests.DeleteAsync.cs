using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Tests;
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
	public async Task DeleteAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.DeleteFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task DeleteAsyncShouldReturnNotFoundWhenCardNotFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, default, default, token))
			.Returns(Task.FromResult<CardEntity?>(null));
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.DeleteNotFound(id));
			cardMock.Verify(x => x.GetByIdAsync(id, default, default, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteAsyncShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		CardEntity card = new();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetByIdAsync(id, default, default, token))
			.Returns(Task.FromResult<CardEntity?>(card));
		cardMock.Setup(x => x.DeleteAsync(card, default))
			.Returns(Task.CompletedTask);
		_repositoryServiceMock.Setup(x => x.CardRepository)
			.Returns(cardMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			cardMock.Verify(x => x.GetByIdAsync(id, default, default, token), Times.Once);
			cardMock.Verify(x => x.DeleteAsync(card, token), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
