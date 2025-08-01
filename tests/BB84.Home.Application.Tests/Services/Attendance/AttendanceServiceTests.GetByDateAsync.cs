using BB84.Home.Application.Contracts.Responses.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Enumerators.Attendance;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Attendance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AttendanceServiceTests
{
	[TestMethod]
	public async Task GetByDateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DateTime date = DateTime.Today;
		string[] parameters = [$"{userId}", $"{date}"];
		_currentUserServiceMock.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<AttendanceResponse> result = await _sut
			.GetByDateAsync(date, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByDateFailed(date));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task GetByDateShouldReturnNotFoundWhenNotFound()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DateTime date = DateTime.Today;
		Mock<IAttendanceRepository> attendanceRepoMock = new();
		attendanceRepoMock.Setup(x => x.GetByConditionAsync(x => x.Date.Equals(date.Date), null, false, false, token))
			.Returns(Task.FromResult<AttendanceEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(attendanceRepoMock.Object);

		ErrorOr<AttendanceResponse> result = await _sut
			.GetByDateAsync(date, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByDateNotFound(date));
			attendanceRepoMock.Verify(x => x.GetByConditionAsync(x => x.Date.Equals(date.Date), null, false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task GetByDateShouldReturnValidResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DateTime date = DateTime.Today;
		AttendanceEntity entity = new() { Id = userId, Date = date.Date, Type = AttendanceType.Workday, StartTime = TimeSpan.MinValue, EndTime = TimeSpan.MinValue, BreakTime = TimeSpan.MinValue };
		Mock<IAttendanceRepository> attendanceRepoMock = new();
		attendanceRepoMock.Setup(x => x.GetByConditionAsync(x => x.Date.Equals(date.Date), null, false, false, token))
			.Returns(Task.FromResult<AttendanceEntity?>(entity));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(attendanceRepoMock.Object);

		ErrorOr<AttendanceResponse> result = await _sut
			.GetByDateAsync(date, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Id.Should().Be(entity.Id);
			result.Value.Date.Should().Be(date.Date);
			result.Value.Type.Should().Be(entity.Type);
			result.Value.StartTime.Should().Be(entity.StartTime);
			result.Value.EndTime.Should().Be(entity.EndTime);
			result.Value.BreakTime.Should().Be(entity.BreakTime);
			attendanceRepoMock.Verify(x => x.GetByConditionAsync(x => x.Date.Equals(date.Date), null, false, false, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
