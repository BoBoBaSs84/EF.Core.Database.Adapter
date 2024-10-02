using Application.Contracts.Requests.Todo;
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
	public async Task CreateListByUserIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		ListCreateRequest request = GetListCreateRequest();
		TodoService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.CreateListByUserId(userId, request)
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
	[TestCategory("Methods")]
	public async Task CreateListByUserIdShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		ListCreateRequest request = GetListCreateRequest();
		Mock<IListRepository> listMock = new();
		TodoService sut = CreateMockedInstance(listMock.Object);

		ErrorOr<Created> result = await sut.CreateListByUserId(userId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			listMock.Verify(x => x.CreateAsync(It.IsAny<List>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}
}
