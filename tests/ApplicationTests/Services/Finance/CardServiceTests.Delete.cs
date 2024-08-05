using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Finance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class CardServiceTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(CardService.Delete))]
	public async Task DeleteShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CardService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.Delete(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.DeleteFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.Delete))]
	public async Task DeleteShouldReturnNotFoundWhenCardNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.DeleteAsync(id, default))
			.Returns(Task.FromResult(0));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<Deleted> result = await sut.Delete(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.DeleteNotFound(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.Delete))]
	public async Task DeleteShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.DeleteAsync(id, default))
			.Returns(Task.FromResult(1));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<Deleted> result = await sut.Delete(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
