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
	public async Task DeleteListAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();

		ErrorOr<Deleted> result = await _sut
			.DeleteListAsync(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.DeleteListByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task DeleteListAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<ListEntity?>(null));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteListAsync(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdNotFound(id));
			listMock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteListAsyncShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		ListEntity list = new();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<ListEntity?>(list));
		listMock.Setup(x => x.DeleteAsync(list, default))
			.Returns(Task.CompletedTask);
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(1));

		ErrorOr<Deleted> result = await _sut
			.DeleteListAsync(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			listMock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			listMock.Verify(x => x.DeleteAsync(list, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Never);
		});
	}
}