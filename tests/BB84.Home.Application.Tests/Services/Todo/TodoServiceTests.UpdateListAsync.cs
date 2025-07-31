using BB84.Extensions;
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
	public async Task UpdateListAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		ListUpdateRequest request = RequestHelper.GetListUpdateRequest();

		ErrorOr<Updated> result = await _sut
			.UpdateListAsync(listId, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.UpdateListByIdFailed(listId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task UpdateListAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		ListUpdateRequest request = RequestHelper.GetListUpdateRequest();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, false, true, default))
			.Returns(Task.FromResult<ListEntity?>(null));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateListAsync(listId, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdNotFound(listId));
			listMock.Verify(x => x.GetByIdAsync(listId, false, true, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task UpdateListAsyncShouldReturnUpdatedWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		ListUpdateRequest request = RequestHelper.GetListUpdateRequest();
		ListEntity model = new();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, false, true, default))
			.Returns(Task.FromResult<ListEntity?>(model));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateListAsync(listId, request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			model.Title.Should().Be(request.Title);
			model.Color.Should().Be(request.Color?.FromRGBHexString());
			listMock.Verify(x => x.GetByIdAsync(listId, false, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
