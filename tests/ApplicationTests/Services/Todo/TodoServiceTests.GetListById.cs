using System.Drawing;

using Application.Contracts.Responses.Todo;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Services.Todo;

using BaseTests.Helpers;

using BB84.Extensions;

using Domain.Errors;
using Domain.Models.Todo;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Todo;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests
{
	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetListByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		TodoService sut = CreateMockedInstance();

		ErrorOr<ListResponse> result = await sut.GetListById(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdFailed(listId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task GetListByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		Mock<IListRepository> listRepositoryMock = new();
		listRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(listId), null, false, false, default)).Returns(Task.FromResult<List?>(null));
		TodoService sut = CreateMockedInstance(listRepositoryMock.Object);

		ErrorOr<ListResponse> result = await sut.GetListById(listId)
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
	public async Task GetListByIdShouldReturnValidResultWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		List list = new() { Title = "Hello", Color = Color.Black };
		Mock<IListRepository> listRepositoryMock = new();
		listRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(listId), null, false, false, default, nameof(List.Items))).Returns(Task.FromResult<List?>(list));
		TodoService sut = CreateMockedInstance(listRepositoryMock.Object);

		ErrorOr<ListResponse> result = await sut.GetListById(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Title.Should().Be(list.Title);
			result.Value.Color.Should().Be(list.Color?.ToRGBHexString());
			result.Value.Items.Should().BeEmpty();
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
