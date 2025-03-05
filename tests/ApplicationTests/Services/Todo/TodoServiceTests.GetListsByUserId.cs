using System.Drawing;

using Application.Contracts.Responses.Todo;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using Application.Services.Todo;

using BaseTests.Helpers;

using BB84.Extensions;
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
	public async Task GetListsByUserIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		TodoService sut = CreateMockedInstance();

		ErrorOr<IEnumerable<ListResponse>> result = await sut.GetListsByUserId(userId)
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
	public async Task GetListsByUserIdShouldReturnValidResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		ListEntity list = new() { Title = "Hello", Color = Color.Red };
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetManyByConditionAsync(x => x.UserId.Equals(userId), null, false, null, null, null, false, default))
			.Returns(Task.FromResult<IEnumerable<ListEntity>>([list]));
		TodoService sut = CreateMockedInstance(listMock.Object);

		ErrorOr<IEnumerable<ListResponse>> result = await sut.GetListsByUserId(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.First().Title.Should().Be(list.Title);
			result.Value.First().Color.Should().Be(list.Color?.ToRGBHexString());
			result.Value.First().Items.Should().BeNull();
			listMock.Verify(x => x.GetManyByConditionAsync(x => x.UserId.Equals(userId), null, false, null, null, null, false, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
