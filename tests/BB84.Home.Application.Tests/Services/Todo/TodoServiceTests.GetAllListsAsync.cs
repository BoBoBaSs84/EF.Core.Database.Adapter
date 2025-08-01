using System.Drawing;

using BB84.Extensions;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Todo;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Todo;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetAllListsAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<IEnumerable<ListResponse>> result = await _sut
			.GetAllListsAsync(token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListsByUserIdFailed(userId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetAllListsAsyncShouldReturnValidResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);
		ListEntity list = new() { Title = "Hello", Color = Color.Red };
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetAllAsync(false, false, token))
			.Returns(Task.FromResult<IEnumerable<ListEntity>>([list]));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<IEnumerable<ListResponse>> result = await _sut
			.GetAllListsAsync(token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.First().Title.Should().Be(list.Title);
			result.Value.First().Color.Should().Be(list.Color?.ToRGBHexString());
			result.Value.First().Items.Should().BeNull();
			listMock.Verify(x => x.GetAllAsync(false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
