using BB84.Home.Application.Errors.Services;
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
	public async Task DeleteMultipleAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		string[] parameters = [$"{string.Join(',', ids)}"];

		ErrorOr<Deleted> result = await _sut.DeleteAsync(ids)
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
	public async Task DeleteMultipleAsyncShouldReturnNotFoundWhenNotFound()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(ids, default, default, default))
			.Returns(Task.FromResult<IEnumerable<AttendanceEntity>>([]));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Deleted> result = await _sut.DeleteAsync(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByIdsNotFound(ids));
			mock.Verify(x => x.GetByIdsAsync(ids, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteMultipleAsyncShouldReturnDeletedWhenSuccessful()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		IEnumerable<AttendanceEntity> attendances = [new(), new()];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(ids, default, default, default))
			.Returns(Task.FromResult(attendances));
		mock.Setup(x => x.DeleteAsync(attendances, default))
			.Returns(Task.CompletedTask);
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(2));

		ErrorOr<Deleted> result = await _sut.DeleteAsync(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			mock.Verify(x => x.GetByIdsAsync(ids, default, default, default), Times.Once);
			mock.Verify(x => x.DeleteAsync(attendances, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
