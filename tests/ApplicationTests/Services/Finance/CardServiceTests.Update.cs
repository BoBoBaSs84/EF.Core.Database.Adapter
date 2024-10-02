using System.Linq.Expressions;

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

using Microsoft.EntityFrameworkCore.Query;
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
		cardMock.Setup(x => x.UpdateAsync(id, It.IsAny<Expression<Func<SetPropertyCalls<CardModel>, SetPropertyCalls<CardModel>>>>(), default))
			.Returns(Task.FromResult(0));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(CardServiceErrors.UpdateNotFound(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(CardService.Update))]
	public async Task UpdateShouldReturnUpdatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CardUpdateRequest request = RequestHelper.GetCardUpdateRequest();
		Mock<ICardRepository> cardMock = new();
		cardMock.Setup(x => x.UpdateAsync(id, It.IsAny<Expression<Func<SetPropertyCalls<CardModel>, SetPropertyCalls<CardModel>>>>(), default))
			.Returns(Task.FromResult(1));
		CardService sut = CreateMockedInstance(cardRepository: cardMock.Object);

		ErrorOr<Updated> result = await sut.Update(id, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
