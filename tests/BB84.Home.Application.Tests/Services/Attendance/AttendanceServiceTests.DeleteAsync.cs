﻿using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Attendance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AttendanceServiceTests
{
	[TestMethod]
	public async Task DeleteByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();

		ErrorOr<Deleted> result = await _sut.DeleteAsync(id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.DeleteByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), id, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task DeleteByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<AttendanceEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Deleted> result = await _sut.DeleteAsync(id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByIdNotFound(id));
			mock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteByIdShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		AttendanceEntity model = new();
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<AttendanceEntity?>(model));
		mock.Setup(x => x.DeleteAsync(model, default))
			.Returns(Task.CompletedTask);
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(0));

		ErrorOr<Deleted> result = await _sut.DeleteAsync(id, default)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			mock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			mock.Verify(x => x.DeleteAsync(model, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
