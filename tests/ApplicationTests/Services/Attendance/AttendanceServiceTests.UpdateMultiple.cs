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
	[TestCategory(nameof(AttendanceService.UpdateMultiple))]
	public async Task UpdateMultipleShouldReturnBadRequestWhenNotValid()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = Guid.NewGuid(), Type = AttendanceType.WORKDAY }];
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
	[TestCategory(nameof(AttendanceService.UpdateMultiple))]
	public async Task UpdateMultipleShouldReturnNotFoundWhenNotFound()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = Guid.NewGuid(), Type = AttendanceType.VACATION }];
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
	[TestCategory(nameof(AttendanceService.UpdateMultiple))]
	public async Task UpdateMultipleShouldReturnCreatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = id, Type = AttendanceType.HOLIDAY }];
		IEnumerable<AttendanceModel> models = [new() { Id = id, Type = AttendanceType.WORKDAY, StartTime = TimeSpan.Zero, EndTime = TimeSpan.Zero, BreakTime = TimeSpan.Zero }];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<bool>(), It.IsAny<bool>(), default))
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
			models.First().Type.Should().Be(requests.First().Type);
			models.First().StartTime.Should().Be(requests.First().StartTime);
			models.First().EndTime.Should().Be(requests.First().EndTime);
			models.First().BreakTime.Should().Be(requests.First().BreakTime);
			mock.Verify(x => x.UpdateAsync(It.IsAny<IEnumerable<AttendanceModel>>()), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AttendanceService.UpdateMultiple))]
	public async Task UpdateMultipleShouldReturnFailedWhenExceptionIsThrown()
	{
		IEnumerable<AttendanceUpdateRequest> requests = [new() { Id = Guid.NewGuid(), Type = AttendanceType.HOLIDAY }];
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
