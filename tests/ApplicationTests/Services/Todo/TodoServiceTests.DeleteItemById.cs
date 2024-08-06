using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Services.Todo;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Todo;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public async Task DeleteItemByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid itemId = Guid.NewGuid();
		TodoService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteItemById(itemId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.DeleteItemByIdFailed(itemId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task DeleteItemByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid itemId = Guid.NewGuid();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.DeleteAsync(itemId, default))
			.Returns(Task.FromResult(0));
		TodoService sut = CreateMockedInstance(itemRepository: itemMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteItemById(itemId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetItemByIdNotFound(itemId));
			itemMock.Verify(x => x.DeleteAsync(itemId, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task DeleteItemByIdShouldReturnDeletedWhenSuccessful()
	{
		Guid itemId = Guid.NewGuid();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.DeleteAsync(itemId, default))
			.Returns(Task.FromResult(1));
		TodoService sut = CreateMockedInstance(itemRepository: itemMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteItemById(itemId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			itemMock.Verify(x => x.DeleteAsync(itemId, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
