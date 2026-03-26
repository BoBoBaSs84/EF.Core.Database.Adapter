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
	[TestCategory(nameof(AttendanceService.UpdateAsync))]
	public async Task UpdateMultipleAsyncShouldReturnNotFoundWhenNotFound()
	{
		IReadOnlyList<AttendanceEntity> emptyList = [];
		IEnumerable<AttendanceUpdateRequest> requests = [RequestHelper.GetAttendanceUpdateRequest()];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>(), It.IsAny<bool>(), _cancellationToken))
			.Returns(Task.FromResult(emptyList));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests, _cancellationToken)
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
	[TestCategory(nameof(AttendanceService.UpdateAsync))]
	public async Task UpdateMultipleAsyncShouldReturnCreatedWhenSuccessful()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [RequestHelper.GetAttendanceUpdateRequest()];
		IReadOnlyList<AttendanceEntity> models = [new() { Id = requests.First().Id, Type = AttendanceType.Vacation }];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>(), It.IsAny<bool>(), _cancellationToken))
			.Returns(Task.FromResult(models));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			models[0].Type.Should().Be(requests.First().Type);
			models[0].StartTime.Should().Be(requests.First().StartTime);
			models[0].EndTime.Should().Be(requests.First().EndTime);
			models[0].BreakTime.Should().Be(requests.First().BreakTime);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(_cancellationToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AttendanceService.UpdateAsync))]
	public async Task UpdateMultipleAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [RequestHelper.GetAttendanceUpdateRequest()];
		string[] parameters = [string.Join(',', requests.Select(x => x.Id))];

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests, _cancellationToken)
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
