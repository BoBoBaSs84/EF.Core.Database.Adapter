using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Application.Tests.Helpers;
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
	public async Task UpdateItemAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid itemId = Guid.NewGuid();
		ItemUpdateRequest request = RequestHelper.GetItemUpdateRequest();

		ErrorOr<Updated> result = await _sut
			.UpdateItemAsync(itemId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.UpdateItemByIdFailed(itemId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task UpdateItemAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid itemId = Guid.NewGuid();
		ItemUpdateRequest request = RequestHelper.GetItemUpdateRequest();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.GetByIdAsync(itemId, false, true, default))
			.Returns(Task.FromResult<ItemEntity?>(null));
		_repositoryServiceMock.Setup(x => x.TodoItemRepository)
			.Returns(itemMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateItemAsync(itemId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetItemByIdNotFound(itemId));
			itemMock.Verify(x => x.GetByIdAsync(itemId, false, true, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task UpdateItemAsyncShouldReturnUpdatedWhenSuccessful()
	{
		Guid itemId = Guid.NewGuid();
		ItemUpdateRequest request = RequestHelper.GetItemUpdateRequest();
		ItemEntity model = new();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.GetByIdAsync(itemId, false, true, default))
			.Returns(Task.FromResult<ItemEntity?>(model));
		_repositoryServiceMock.Setup(x => x.TodoItemRepository)
			.Returns(itemMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateItemAsync(itemId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			model.Title.Should().Be(request.Title);
			model.Note.Should().Be(request.Note);
			model.Priority.Should().Be(request.Priority);
			model.Reminder.Should().Be(request.Reminder);
			model.Done.Should().Be(request.Done);
			itemMock.Verify(x => x.GetByIdAsync(itemId, false, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
