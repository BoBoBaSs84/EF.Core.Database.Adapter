using Application.Contracts.Requests.Attendance;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Attendance;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

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
	public async Task CreateMultipleShouldReturnConflictWhenExistingEntriesFound()
	{
		Guid id = Guid.NewGuid();
		IEnumerable<AttendanceCreateRequest> requests = [RequestHelper.GetAttendanceCreateRequest()];
		IEnumerable<AttendanceModel> models = [new() { Date = DateTime.Today }];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetManyByConditionAsync(x => x.UserId.Equals(id) && requests.Select(x => x.Date).Contains(x.Date), null, false, null, null, null, false, default))
			.Returns(Task.FromResult(models));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Created> result = await sut.CreateMultiple(id, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateMultipleConflict(models.Select(x => x.Date)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task CreateMultipleShouldReturnCreatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		IEnumerable<AttendanceCreateRequest> requests = [RequestHelper.GetAttendanceCreateRequest()];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetManyByConditionAsync(x => x.UserId.Equals(id) && requests.Select(x => x.Date).Contains(x.Date), null, false, null, null, null, false, default))
			.Returns(Task.FromResult<IEnumerable<AttendanceModel>>([]));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Created> result = await sut.CreateMultiple(id, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			mock.Verify(x => x.CreateAsync(It.IsAny<IEnumerable<AttendanceModel>>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task CreateMultipleShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		IEnumerable<AttendanceCreateRequest> requests = [RequestHelper.GetAttendanceCreateRequest()];
		string[] parameters = [$"{id}", string.Join(',', requests.Select(x => x.Date))];
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.CreateMultiple(id, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateMultipleFailed(requests.Select(x => x.Date)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}
}
