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
	public async Task CreateItemAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = RequestHelper.GetItemCreateRequest();

		ErrorOr<Created> result = await _sut
			.CreateItemAsync(listId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.CreateItemByListIdFailed(listId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task CreateItemAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = RequestHelper.GetItemCreateRequest();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, false, false, default))
			.Returns(Task.FromResult<ListEntity?>(null));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateItemAsync(listId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdNotFound(listId));
			listMock.Verify(x => x.GetByIdAsync(listId, false, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateItemAsyncShouldReturnCreatedWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = RequestHelper.GetItemCreateRequest();
		Mock<IListRepository> listMock = new();
		Mock<IItemRepository> itemMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, false, false, default))
			.Returns(Task.FromResult<ListEntity?>(new()));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateItemAsync(listId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			listMock.Verify(x => x.GetByIdAsync(listId, false, false, default), Times.Once);
			itemMock.Verify(x => x.CreateAsync(It.IsAny<ItemEntity>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}
}
