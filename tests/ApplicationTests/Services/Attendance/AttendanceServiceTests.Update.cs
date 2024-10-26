using Application.Contracts.Requests.Attendance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Attendance;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Enumerators.Attendance;
using Domain.Errors;
using Domain.Models.Attendance;
using Domain.Results;

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
			.Returns(Task.FromResult<AttendanceModel?>(null));
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
		AttendanceModel model = new() { Type = AttendanceType.WORKDAY, StartTime = TimeSpan.Zero, EndTime = TimeSpan.Zero, BreakTime = TimeSpan.Zero };
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdAsync(request.Id, false, true, default))
			.Returns(Task.FromResult<AttendanceModel?>(model));
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
