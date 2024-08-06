using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Services.Todo;

using BaseTests.Helpers;

using Domain.Errors;
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
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.DeleteAsync(listId, default))
			.Returns(Task.FromResult(0));
		TodoService sut = CreateMockedInstance(listMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteListById(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdNotFound(listId));
			listMock.Verify(x => x.DeleteAsync(listId, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task DeleteListByIdShouldReturnDeletedWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.DeleteAsync(listId, default))
			.Returns(Task.FromResult(1));
		TodoService sut = CreateMockedInstance(listMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteListById(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			listMock.Verify(x => x.DeleteAsync(listId, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}
}