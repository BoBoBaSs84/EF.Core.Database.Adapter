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
	public async Task CreateShouldReturnConflictWhenExistingEntryFound()
	{
		Guid id = Guid.NewGuid();
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		AttendanceModel model = new() { Date = DateTime.Today };
		string[] parameters = [$"{id}", $"{request.Date}"];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(x => x.UserId.Equals(id) && x.Date.Equals(request.Date), null, false, false, default))
			.Returns(Task.FromResult<AttendanceModel?>(model));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Created> result = await sut.Create(id, request)
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
	[TestCategory("Method")]
	public async Task CreateShouldReturnCreatedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		string[] parameters = [$"{id}", $"{request.Date}"];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByConditionAsync(x => x.UserId.Equals(id) && x.Date.Equals(request.Date), null, false, false, default))
			.Returns(Task.FromResult<AttendanceModel?>(null));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Created> result = await sut.Create(id, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			mock.Verify(x => x.CreateAsync(It.IsAny<AttendanceModel>(), default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task CreateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		AttendanceCreateRequest request = RequestHelper.GetAttendanceCreateRequest();
		string[] parameters = [$"{id}", $"{request.Date}"];
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.Create(id, request)
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
