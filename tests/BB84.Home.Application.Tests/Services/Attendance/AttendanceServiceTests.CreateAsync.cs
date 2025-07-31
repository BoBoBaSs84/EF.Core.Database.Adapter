using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Attendance;
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
	public async Task CreateAsyncShouldReturnConflictWhenExistingEntryFound()
	{
		Guid id = Guid.NewGuid();
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		AttendanceEntity model = new() { Date = DateTime.Today };
		string[] parameters = [$"{id}", $"{request.Date}"];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(x => x.UserId.Equals(id) && x.Date.Equals(request.Date), null, false, false, default))
			.Returns(Task.FromResult<AttendanceEntity?>(model));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Created> result = await _sut.CreateAsync(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateConflict(request.Date));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnCreatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		string[] parameters = [$"{id}", $"{request.Date}"];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(x => x.UserId.Equals(id) && x.Date.Equals(request.Date), null, false, false, default))
			.Returns(Task.FromResult<AttendanceEntity?>(null));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<Created> result = await _sut.CreateAsync(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			mock.Verify(x => x.CreateAsync(It.IsAny<AttendanceEntity>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		string[] parameters = [$"{id}", $"{request.Date}"];

		ErrorOr<Created> result = await _sut.CreateAsync(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.CreateFailed(request.Date));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}
}
