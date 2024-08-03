using Application.Contracts.Responses.Attendance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Attendance;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Models.Attendance;

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

		ErrorOr<AttendanceResponse> result = await sut.GetByDate(userId, date)
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
			.Returns(Task.FromResult<AttendanceModel?>(null));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<AttendanceResponse> result = await sut.GetByDate(userId, date)
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
		AttendanceModel model = new() { Id = userId, Date = date.Date, Type = AttendanceType.WORKDAY, StartTime = TimeSpan.MinValue, EndTime = TimeSpan.MinValue, BreakTime = TimeSpan.MinValue };
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(x => x.UserId.Equals(userId) && x.Date.Equals(date.Date), null, false, false, default))
			.Returns(Task.FromResult<AttendanceModel?>(model));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<AttendanceResponse> result = await sut.GetByDate(userId, date)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Id.Should().Be(model.Id);
			result.Value.Date.Should().Be(date.Date);
			result.Value.AttendanceType.Should().Be(model.Type);
			result.Value.StartTime.Should().Be(model.StartTime);
			result.Value.EndTime.Should().Be(model.EndTime);
			result.Value.BreakTime.Should().Be(model.BreakTime);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
