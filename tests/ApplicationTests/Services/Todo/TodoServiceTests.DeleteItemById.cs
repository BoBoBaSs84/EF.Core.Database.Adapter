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
		Mock<IItemRepository> itemRepositoryMock = new();
		itemRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(itemId), null, false, false, default)).Returns(Task.FromResult<Item?>(null));
		TodoService sut = CreateMockedInstance(null, itemRepositoryMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteItemById(itemId)
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
	public async Task DeleteItemByIdShouldReturnDeletedWhenSuccessful()
	{
		Guid itemId = Guid.NewGuid();
		Mock<IItemRepository> itemRepositoryMock = new();
		itemRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(itemId), null, false, false, default)).Returns(Task.FromResult<Item?>(new()));
		TodoService sut = CreateMockedInstance(null, itemRepositoryMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteItemById(itemId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			itemRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Item>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), itemId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
