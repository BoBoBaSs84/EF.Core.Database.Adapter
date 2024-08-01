using Application.Contracts.Requests.Todo;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Services.Todo;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Todo;
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
		ItemUpdateRequest request = new();
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
		ItemUpdateRequest request = new();
		Mock<IItemRepository> itemRepositoryMock = new();
		itemRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(itemId), null, false, false, default)).Returns(Task.FromResult<Item?>(null));
		TodoService sut = CreateMockedInstance(null, itemRepositoryMock.Object);

		ErrorOr<Updated> result = await sut.UpdateItemById(itemId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetItemByIdNotFound(itemId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task UpdateItemByIdShouldReturnUpdatedWhenSuccessful()
	{
		Guid itemId = Guid.NewGuid();
		ItemUpdateRequest request = new();
		Mock<IItemRepository> itemRepositoryMock = new();
		itemRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(itemId), null, false, true, default)).Returns(Task.FromResult<Item?>(new()));
		TodoService sut = CreateMockedInstance(null, itemRepositoryMock.Object);

		ErrorOr<Updated> result = await sut.UpdateItemById(itemId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
