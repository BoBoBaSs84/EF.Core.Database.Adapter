using System.Linq.Expressions;

using Application.Contracts.Responses.Finance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Finance;

using BaseTests.Helpers;

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
	[TestCategory(nameof(CardService.GetByUserId))]
	public async Task GetByUserIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CardService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<CardResponse>> result = await sut.GetByUserId(id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.GetByUserIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.GetByUserId))]
	public async Task GetByUserIdShouldReturnResponseWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		IEnumerable<CardModel> cards = [new(), new(), new()];
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.GetManyByConditionAsync(It.IsAny<Expression<Func<CardModel, bool>>>(), null, false, null, null, null, false, default))
			.Returns(Task.FromResult(cards));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<IEnumerable<CardResponse>> result = await sut.GetByUserId(id);

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
