using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Tests.Helpers;
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
	public async Task CreateMultipleAsyncShouldReturnConflictWhenExistingEntriesFound()
	{
		CancellationToken token = CancellationToken.None;
		IEnumerable<AttendanceCreateRequest> requests = [RequestHelper.GetAttendanceCreateRequest()];
		IEnumerable<AttendanceEntity> entities = [new() { Date = DateTime.Today }];
		Mock<IAttendanceRepository> attendanceRepoMock = new();
		attendanceRepoMock.Setup(x => x.GetManyByConditionAsync(x => requests.Select(x => x.Date).Contains(x.Date), null, false, null, null, null, false, token))
			.Returns(Task.FromResult(entities));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(attendanceRepoMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateMultipleConflict(entities.Select(x => x.Date)));
			attendanceRepoMock.Verify(x => x.GetManyByConditionAsync(x => requests.Select(x => x.Date).Contains(x.Date), null, false, null, null, null, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateMultipleAsyncShouldReturnCreatedWhenSuccessful()
	{
		CancellationToken token = CancellationToken.None;
		IEnumerable<AttendanceCreateRequest> requests = [RequestHelper.GetAttendanceCreateRequest()];
		Mock<IAttendanceRepository> attendanceRepoMock = new();
		attendanceRepoMock.Setup(x => x.GetManyByConditionAsync(x => requests.Select(x => x.Date).Contains(x.Date), null, false, null, null, null, false, token))
			.Returns(Task.FromResult<IEnumerable<AttendanceEntity>>([]));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(attendanceRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Created> result = await _sut
			.CreateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			attendanceRepoMock.Verify(x => x.CreateAsync(It.IsAny<IEnumerable<AttendanceEntity>>(), token), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateMultipleAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<AttendanceCreateRequest> requests = [RequestHelper.GetAttendanceCreateRequest()];
		string[] parameters = [$"{userId}", string.Join(',', requests.Select(x => x.Date))];
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<Created> result = await _sut
			.CreateAsync(requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateMultipleFailed(requests.Select(x => x.Date)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}
}
