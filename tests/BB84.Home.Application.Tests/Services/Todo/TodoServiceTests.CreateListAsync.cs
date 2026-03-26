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
	[TestCategory(nameof(TodoService.CreateListAsync))]
	public async Task CreateListAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		ListCreateRequest request = RequestHelper.GetListCreateRequest();
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<Created> result = await _sut
			.CreateListAsync(request, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.CreateListByUserFailed(userId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(TodoService.CreateListAsync))]
	public async Task CreateListAsyncShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		ListCreateRequest request = RequestHelper.GetListCreateRequest();
		Mock<IListRepository> listMock = new();
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(_cancellationToken))
			.Returns(Task.FromResult(1));

		ErrorOr<Created> result = await _sut
			.CreateListAsync(request, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			listMock.Verify(x => x.CreateAsync(It.IsAny<ListEntity>(), _cancellationToken), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(_cancellationToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}
}
