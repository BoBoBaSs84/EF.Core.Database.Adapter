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
	public async Task UpdateMultipleShouldReturnBadRequestWhenNotValid()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.WORKDAY }];
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.UpdateMultiple(requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.UpdateMultipleBadRequest(requests.Select(x => x.Id)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task UpdateMultipleShouldReturnNotFoundWhenNotFound()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.VACATION }];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(requests.Select(x => x.Id), false, true, default))
			.Returns(Task.FromResult<IEnumerable<AttendanceModel>>([]));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Updated> result = await sut.UpdateMultiple(requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByIdsNotFound(requests.Select(x => x.Id)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task UpdateMultipleShouldReturnCreatedWhenSuccessful()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.HOLIDAY }];
		IEnumerable<AttendanceModel> models = [new() { Type = AttendanceType.WORKDAY, StartTime = TimeSpan.Zero, EndTime = TimeSpan.Zero, BreakTime = TimeSpan.Zero }];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(requests.Select(x => x.Id), false, true, default))
			.Returns(Task.FromResult(models));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Updated> result = await sut.UpdateMultiple(requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			mock.Verify(x => x.UpdateAsync(It.IsAny<IEnumerable<AttendanceModel>>()), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task UpdateMultipleShouldReturnFailedWhenExceptionIsThrown()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = Guid.NewGuid(), AttendanceType = AttendanceType.HOLIDAY }];
		string[] parameters = [string.Join(',', requests.Select(x => x.Id))];
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.UpdateMultiple(requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.UpdateMultipleFailed(requests.Select(x => x.Id)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}
}
