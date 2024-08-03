using Application.Contracts.Requests.Attendance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Attendance;

using BaseTests.Helpers;

using Domain.Enumerators;
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
	[TestCategory("Method")]
	public async Task UpdateShouldReturnBadRequestWhenNotValid()
	{
		AttendanceUpdateRequest request = new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.WORKDAY };
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.Update(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.UpdateBadRequest(request.Id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task UpdateShouldReturnNotFoundWhenNotFound()
	{
		AttendanceUpdateRequest request = new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.HOLIDAY };
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
	[TestCategory("Method")]
	public async Task UpdateShouldReturnCreatedWhenSuccessful()
	{
		AttendanceUpdateRequest request = new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.HOLIDAY };
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
			mock.Verify(x => x.UpdateAsync(It.IsAny<AttendanceModel>()), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task UpdateShouldReturnFailedWhenExceptionIsThrown()
	{
		AttendanceUpdateRequest request = new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.HOLIDAY };
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
