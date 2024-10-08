﻿using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Attendance;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Attendance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AttendanceServiceTests
{
	[TestMethod]
	[TestCategory("Method")]
	public async Task DeleteByIdsShouldReturnFailedWhenExceptionIsThrown()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		string[] parameters = [$"{string.Join(',', ids)}"];
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteByIds(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.DeleteByIdsFailed(ids));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task DeleteByIdsShouldReturnNotFoundWhenNotFound()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		string[] parameters = [$"{string.Join(", ", ids)}"];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.DeleteAsync(ids, default))
			.Returns(Task.FromResult(0));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByIds(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByIdsNotFound(ids));
			mock.Verify(x => x.DeleteAsync(ids, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task DeleteByIdsShouldReturnDeletedWhenSuccessful()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		string[] parameters = [$"{string.Join(", ", ids)}"];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.DeleteAsync(ids, default))
			.Returns(Task.FromResult(1));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByIds(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			mock.Verify(x => x.DeleteAsync(ids, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
