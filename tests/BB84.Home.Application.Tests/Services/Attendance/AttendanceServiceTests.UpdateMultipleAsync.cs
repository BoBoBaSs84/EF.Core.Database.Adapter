using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
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
	public async Task UpdateMultipleAsyncShouldReturnNotFoundWhenNotFound()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [RequestHelper.GetAttendanceUpdateRequest()];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>(), It.IsAny<bool>(), default))
			.Returns(Task.FromResult<IEnumerable<AttendanceEntity>>([]));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests)
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
	public async Task UpdateMultipleAsyncShouldReturnCreatedWhenSuccessful()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [RequestHelper.GetAttendanceUpdateRequest()];
		IEnumerable<AttendanceEntity> models = [new() { Id = requests.First().Id, Type = AttendanceType.Vacation }];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>(), It.IsAny<bool>(), default))
			.Returns(Task.FromResult(models));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			models.First().Type.Should().Be(requests.First().Type);
			models.First().StartTime.Should().Be(requests.First().StartTime);
			models.First().EndTime.Should().Be(requests.First().EndTime);
			models.First().BreakTime.Should().Be(requests.First().BreakTime);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task UpdateMultipleAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [RequestHelper.GetAttendanceUpdateRequest()];
		string[] parameters = [string.Join(',', requests.Select(x => x.Id))];

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests)
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
