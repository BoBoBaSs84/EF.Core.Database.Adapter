﻿using Application.Contracts.Requests.Todo;
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
	public async Task CreateItemByListIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = new();
		TodoService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.CreateItemByListId(listId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.CreateItemByListIdFailed(listId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task CreateItemByListIdShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = new();
		Mock<IListRepository> listRepositoryMock = new();
		listRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(listId), null, false, false, default)).Returns(Task.FromResult<List?>(null));
		TodoService sut = CreateMockedInstance(listRepositoryMock.Object);

		ErrorOr<Created> result = await sut.CreateItemByListId(listId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdNotFound(listId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Methods")]
	public async Task CreateItemByListIdShouldReturnCreatedWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		ItemCreateRequest request = new();
		Mock<IListRepository> listRepositoryMock = new();
		Mock<IItemRepository> itemRepositoryMock = new();
		listRepositoryMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(listId), null, false, false, default)).Returns(Task.FromResult<List?>(new()));
		TodoService sut = CreateMockedInstance(listRepositoryMock.Object, itemRepositoryMock.Object);

		ErrorOr<Created> result = await sut.CreateItemByListId(listId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			itemRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Item>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Never);
		});
	}
}
