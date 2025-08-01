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
	public async Task CreateAsyncShouldReturnConflictWhenExistingEntryFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		AttendanceEntity entity = new() { Date = DateTime.Today };
		string[] parameters = [$"{id}", $"{request.Date}"];
		Mock<IAttendanceRepository> attendanceRepoMock = new();
		attendanceRepoMock.Setup(x => x.GetByConditionAsync(x => x.Date.Equals(request.Date), null, false, false, token))
			.Returns(Task.FromResult<AttendanceEntity?>(entity));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(attendanceRepoMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateConflict(request.Date));
			attendanceRepoMock.Verify(x => x.GetByConditionAsync(x => x.Date.Equals(request.Date), null, false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnCreatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		string[] parameters = [$"{id}", $"{request.Date}"];
		Mock<IAttendanceRepository> attendanceRepoMock = new();
		attendanceRepoMock.Setup(x => x.GetByConditionAsync(x => x.Date.Equals(request.Date), null, false, false, token))
			.Returns(Task.FromResult<AttendanceEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(attendanceRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			attendanceRepoMock.Verify(x => x.GetByConditionAsync(x => x.Date.Equals(request.Date), null, false, false, token), Times.Once);
			attendanceRepoMock.Verify(x => x.CreateAsync(It.IsAny<AttendanceEntity>(), token), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		string[] parameters = [$"{userId}", $"{request.Date}"];
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<Created> result = await _sut
			.CreateAsync(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateFailed(request.Date));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}
}
