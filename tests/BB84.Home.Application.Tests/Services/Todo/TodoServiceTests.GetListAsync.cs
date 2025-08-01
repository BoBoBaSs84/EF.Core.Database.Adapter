﻿using System.Drawing;

using BB84.Extensions;
using BB84.Home.Application.Contracts.Responses.Todo;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Todo;
using BB84.Home.Domain.Enumerators.Todo;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Todo;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TodoServiceTests
{
	[TestMethod]
	public async Task GetListAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid listId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;

		ErrorOr<ListResponse> result = await _sut
			.GetListAsync(listId, token)
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
	public async Task GetListAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid listId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, default, default, token, nameof(ListEntity.Items)))
			.Returns(Task.FromResult<ListEntity?>(null));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<ListResponse> result = await _sut
			.GetListAsync(listId, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(TodoServiceErrors.GetListByIdNotFound(listId));
			listMock.Verify(x => x.GetByIdAsync(listId, default, default, token, nameof(ListEntity.Items)), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task GetListAsyncShouldReturnValidResultWhenSuccessful()
	{
		Guid listId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		ItemEntity item = new() { Id = Guid.NewGuid(), Title = "UnitTest", Note = "UnitTest", Priority = PriorityLevelType.Medium, Reminder = DateTime.Today, Done = true };
		ListEntity list = new() { Id = listId, Title = "UnitTest", Color = Color.Black, Items = [item] };
		Mock<IListRepository> listMock = new();
		listMock.Setup(x => x.GetByIdAsync(listId, default, default, token, nameof(ListEntity.Items)))
			.Returns(Task.FromResult<ListEntity?>(list));
		_repositoryServiceMock.Setup(x => x.TodoListRepository)
			.Returns(listMock.Object);

		ErrorOr<ListResponse> result = await _sut
			.GetListAsync(listId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Title.Should().Be(list.Title);
			result.Value.Color.Should().Be(list.Color?.ToRGBHexString());
			result.Value.Items.Should().NotBeNullOrEmpty();
			result.Value.Items?.First().Id.Should().Be(item.Id);
			result.Value.Items?.First().Title.Should().Be(item.Title);
			result.Value.Items?.First().Note.Should().Be(item.Note);
			result.Value.Items?.First().Priority.Should().Be(item.Priority);
			result.Value.Items?.First().Reminder.Should().Be(item.Reminder);
			result.Value.Items?.First().Done.Should().Be(item.Done);
			listMock.Verify(x => x.GetByIdAsync(listId, default, default, token, nameof(ListEntity.Items)), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), listId, It.IsAny<Exception>()), Times.Never);
		});
	}
}
