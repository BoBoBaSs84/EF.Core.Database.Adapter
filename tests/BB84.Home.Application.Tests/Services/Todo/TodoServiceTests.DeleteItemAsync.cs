using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Todo;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Todo;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests
{
	[TestMethod]
	public async Task DeleteItemAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();

		ErrorOr<Deleted> result = await _sut
			.DeleteItemAsync(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.DeleteItemByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task DeleteItemAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<ItemEntity?>(null));
		_repositoryServiceMock.Setup(x => x.TodoItemRepository)
			.Returns(itemMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteItemAsync(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetItemByIdNotFound(id));
			itemMock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteItemAsyncShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		ItemEntity item = new();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<ItemEntity?>(item));
		itemMock.Setup(x => x.DeleteAsync(item, default))
			.Returns(Task.CompletedTask);
		_repositoryServiceMock.Setup(x => x.TodoItemRepository)
			.Returns(itemMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(1));

		ErrorOr<Deleted> result = await _sut
			.DeleteItemAsync(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			itemMock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			itemMock.Verify(x => x.DeleteAsync(item, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
