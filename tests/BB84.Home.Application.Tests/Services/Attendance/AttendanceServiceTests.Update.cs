using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Attendance;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Enumerators.Attendance;
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
	[TestCategory(nameof(AttendanceService.Update))]
	public async Task UpdateShouldReturnNotFoundWhenNotFound()
	{
		AttendanceUpdateRequest request = RequestHelper.GetAttendanceUpdateRequest();
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdAsync(request.Id, false, true, default))
			.Returns(Task.FromResult<AttendanceEntity?>(null));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Updated> result = await sut.Update(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByIdNotFound(request.Id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AttendanceService.Update))]
	public async Task UpdateShouldReturnCreatedWhenSuccessful()
	{
		AttendanceUpdateRequest request = RequestHelper.GetAttendanceUpdateRequest();
		AttendanceEntity model = new() { Type = AttendanceType.Workday, StartTime = TimeSpan.Zero, EndTime = TimeSpan.Zero, BreakTime = TimeSpan.Zero };
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdAsync(request.Id, false, true, default))
			.Returns(Task.FromResult<AttendanceEntity?>(model));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Updated> result = await sut.Update(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			model.Type.Should().Be(request.Type);
			model.StartTime.Should().Be(request.StartTime);
			model.EndTime.Should().Be(request.EndTime);
			model.BreakTime.Should().Be(request.BreakTime);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AttendanceService.Update))]
	public async Task UpdateShouldReturnFailedWhenExceptionIsThrown()
	{
		AttendanceUpdateRequest request = RequestHelper.GetAttendanceUpdateRequest();
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.Update(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.UpdateFailed(request.Id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), request, It.IsAny<Exception>()), Times.Once);
		});
	}
}
