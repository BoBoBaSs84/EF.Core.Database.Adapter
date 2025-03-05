using BaseTests.Helpers;

using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Attendance;
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
	[TestCategory("Method")]
	public async Task DeleteByIdsShouldReturnFailedWhenExceptionIsThrown()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		string[] parameters = [$"{string.Join(',', ids)}"];
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteByIds(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.DeleteByIdsFailed(ids));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task DeleteByIdsShouldReturnNotFoundWhenNotFound()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(ids, default, default, default))
			.Returns(Task.FromResult<IEnumerable<AttendanceEntity>>([]));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByIds(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetByIdsNotFound(ids));
			mock.Verify(x => x.GetByIdsAsync(ids, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory("Method")]
	public async Task DeleteByIdsShouldReturnDeletedWhenSuccessful()
	{
		IEnumerable<Guid> ids = [Guid.NewGuid(), Guid.NewGuid()];
		IEnumerable<AttendanceEntity> attendances = [new(), new()];
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetByIdsAsync(ids, default, default, default))
			.Returns(Task.FromResult(attendances));
		mock.Setup(x => x.DeleteAsync(attendances, default))
			.Returns(Task.CompletedTask);
		AttendanceService sut = CreateMockedInstance(mock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(default))
			.Returns(Task.FromResult(2));

		ErrorOr<Deleted> result = await sut.DeleteByIds(ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			mock.Verify(x => x.GetByIdsAsync(ids, default, default, default), Times.Once);
			mock.Verify(x => x.DeleteAsync(attendances, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
