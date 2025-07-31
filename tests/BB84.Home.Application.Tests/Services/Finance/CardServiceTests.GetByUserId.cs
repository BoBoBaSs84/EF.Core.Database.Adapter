using System.Linq.Expressions;

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
	[TestCategory(nameof(CardService.GetAllAsync))]
	public async Task GetByUserIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CardService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<CardResponse>> result = await sut.GetAll(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.GetByUserIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.GetAllAsync))]
	public async Task GetByUserIdShouldReturnResponseWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		IEnumerable<CardEntity> cards = [new(), new(), new()];
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardEntity, bool>>>(), null, false, null, null, null, false, default))
			.Returns(Task.FromResult(cards));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<IEnumerable<CardResponse>> result = await sut.GetAll(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Count().Should().Be(cards.Count());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
