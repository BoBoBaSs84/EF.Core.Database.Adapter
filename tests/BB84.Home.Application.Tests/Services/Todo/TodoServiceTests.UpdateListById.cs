using BB84.Extensions;
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
	public async Task UpdateListByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		ListUpdateRequest request = RequestHelper.GetListUpdateRequest();
		TodoService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.UpdateListAsync(listId, request)
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
	[TestCategory("Methods")]
	public async Task UpdateListByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		ListUpdateRequest request = RequestHelper.GetListUpdateRequest();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, false, true, default))
			.Returns(Task.FromResult<ListEntity?>(null));
		TodoService sut = CreateMockedInstance(listMock.Object);

		ErrorOr<Updated> result = await sut.UpdateListAsync(listId, request)
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
	[TestCategory("Methods")]
	public async Task UpdateListByIdShouldReturnUpdatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		ListUpdateRequest request = RequestHelper.GetListUpdateRequest();
		ListEntity model = new();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(id, false, true, default))
			.Returns(Task.FromResult<ListEntity?>(model));
		TodoService sut = CreateMockedInstance(listMock.Object);

		ErrorOr<Updated> result = await sut.UpdateListAsync(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			model.Title.Should().Be(request.Title);
			model.Color.Should().Be(request.Color?.FromRGBHexString());
			listMock.Verify(x => x.GetByIdAsync(id, false, true, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}
