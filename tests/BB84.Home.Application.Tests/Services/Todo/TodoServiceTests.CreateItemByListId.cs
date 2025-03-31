using BB84.Home.Application.Contracts.Requests.Todo;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Application.Services.Todo;
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
	[TestCategory("Methods")]
	public async Task CreateItemByListIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = RequestHelper.GetItemCreateRequest();
		TodoService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.CreateItemByListId(listId, request)
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
	[TestCategory("Methods")]
	public async Task CreateItemByListIdShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = RequestHelper.GetItemCreateRequest();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, false, false, default))
			.Returns(Task.FromResult<ListEntity?>(null));
		TodoService sut = CreateMockedInstance(listMock.Object);

		ErrorOr<Created> result = await sut.CreateItemByListId(listId, request)
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
	[TestCategory("Methods")]
	public async Task CreateItemByListIdShouldReturnCreatedWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = RequestHelper.GetItemCreateRequest();
		Mock<IListRepository> listMock = new();
		Mock<IItemRepository> itemMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, false, false, default))
			.Returns(Task.FromResult<ListEntity?>(new()));
		TodoService sut = CreateMockedInstance(listMock.Object, itemMock.Object);

		ErrorOr<Created> result = await sut.CreateItemByListId(listId, request)
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
