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
		List list = new() { Title = "Hello", Color = Color.Black };
		Mock<IListRepository> listRepositoryMock = new();
		listRepositoryMock.Setup(x => x.GetManyByConditionAsync(
			x => x.Users.Select(x => x.UserId).Contains(userId), null, false, null, null, null, false, default)
		).Returns(Task.FromResult<IEnumerable<List>>([list]));
		TodoService sut = CreateMockedInstance(listRepositoryMock.Object);

		ErrorOr<IEnumerable<ListResponse>> result = await sut.GetListsByUserId(userId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.First().Title.Should().Be(list.Title);
			result.Value.First().Color.Should().Be(list.Color?.ToRGBHexString());
			result.Value.First().Items.Should().BeEmpty();
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), userId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
