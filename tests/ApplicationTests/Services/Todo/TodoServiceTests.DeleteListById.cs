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
	public async Task DeleteListByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		TodoService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteListById(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.DeleteListByIdFailed(listId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task DeleteListByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		Mock<IListRepository> listRepositoryMock = new();
		listRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(listId), null, false, false, default)).Returns(Task.FromResult<List?>(null));
		TodoService sut = CreateMockedInstance(listRepositoryMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteListById(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdNotFound(listId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task DeleteListByIdShouldReturnDeletedWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		Mock<IListRepository> listRepositoryMock = new();
		listRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(listId), null, false, false, default)).Returns(Task.FromResult<List?>(new()));
		TodoService sut = CreateMockedInstance(listRepositoryMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteListById(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			listRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<List>()), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}
}