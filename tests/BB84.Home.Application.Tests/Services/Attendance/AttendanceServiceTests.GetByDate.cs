using BB84.Home.Application.Contracts.Responses.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Attendance;
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
	[TestCategory("Method")]
	public async Task GetByDateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		DateTime date = DateTime.Today;
		string[] parameters = [$"{userId}", $"{date}"];
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<AttendanceResponse> result = await sut.GetByUserIdAndDate(userId, date)
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
	[TestCategory("Method")]
	public async Task GetByDateShouldReturnNotFoundWhenNotFound()
	{
		Guid userId = Guid.NewGuid();
		DateTime date = DateTime.Today;
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(x => x.UserId.Equals(userId) && x.Date.Equals(date.Date), null, false, false, default))
			.Returns(Task.FromResult<AttendanceEntity?>(null));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<AttendanceResponse> result = await sut.GetByUserIdAndDate(userId, date)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByDateNotFound(date));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task GetByDateShouldReturnValidResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		DateTime date = DateTime.Today;
		AttendanceEntity model = new() { Id = userId, Date = date.Date, Type = AttendanceType.Workday, StartTime = TimeSpan.MinValue, EndTime = TimeSpan.MinValue, BreakTime = TimeSpan.MinValue };
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(x => x.UserId.Equals(userId) && x.Date.Equals(date.Date), null, false, false, default))
			.Returns(Task.FromResult<AttendanceEntity?>(model));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<AttendanceResponse> result = await sut.GetByUserIdAndDate(userId, date)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Id.Should().Be(model.Id);
			result.Value.Date.Should().Be(date.Date);
			result.Value.Type.Should().Be(model.Type);
			result.Value.StartTime.Should().Be(model.StartTime);
			result.Value.EndTime.Should().Be(model.EndTime);
			result.Value.BreakTime.Should().Be(model.BreakTime);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
