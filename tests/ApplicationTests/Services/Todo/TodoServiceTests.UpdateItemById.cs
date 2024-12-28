using Application.Contracts.Requests.Todo;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Services.Todo;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Entities.Todo;
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
	public async Task UpdateItemByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid itemId = Guid.NewGuid();
		ItemUpdateRequest request = RequestHelper.GetItemUpdateRequest();
		TodoService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.UpdateItemById(itemId, request)
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
	[TestCategory("Methods")]
	public async Task UpdateItemByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid itemId = Guid.NewGuid();
		ItemUpdateRequest request = RequestHelper.GetItemUpdateRequest();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.GetByIdAsync(itemId, false, true, default))
			.Returns(Task.FromResult<ItemEntity?>(null));
		TodoService sut = CreateMockedInstance(itemRepository: itemMock.Object);

		ErrorOr<Updated> result = await sut.UpdateItemById(itemId, request)
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
	[TestCategory("Methods")]
	public async Task UpdateItemByIdShouldReturnUpdatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		ItemUpdateRequest request = RequestHelper.GetItemUpdateRequest();
		ItemEntity model = new();
		Mock<IItemRepository> itemMock = new();
		itemMock.Setup(x => x.GetByIdAsync(id, false, true, default))
			.Returns(Task.FromResult<ItemEntity?>(model));
		TodoService sut = CreateMockedInstance(itemRepository: itemMock.Object);

		ErrorOr<Updated> result = await sut.UpdateItemById(id, request)
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
			itemMock.Verify(x => x.GetByIdAsync(id, false, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
