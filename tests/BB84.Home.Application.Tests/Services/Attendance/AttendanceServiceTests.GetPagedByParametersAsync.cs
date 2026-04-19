using BB84.Home.Application.Contracts.Responses.Attendance;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Services.Attendance;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Attendance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AttendanceServiceTests
{
	[TestMethod]
	[TestCategory(nameof(AttendanceService.GetPagedByParametersAsync))]
	public async Task GetPagedByParametersShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		AttendanceParameters parameters = new();

		ErrorOr<IPagedList<AttendanceResponse>> result = await _sut
			.GetPagedByParametersAsync(parameters, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(AttendanceServiceErrors.GetPagedByParametersFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(AttendanceService.GetPagedByParametersAsync))]
	public async Task GetPagedByParametersShouldReturnResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		IReadOnlyList<AttendanceEntity> attendances = [];
		AttendanceParameters parameters = new();
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.GetManyByConditionAsync(
			It.IsAny<Func<IQueryable<AttendanceEntity>, IQueryable<AttendanceEntity>>>(),
			false, It.IsAny<Func<IQueryable<AttendanceEntity>, IOrderedQueryable<AttendanceEntity>>?>(),
			(parameters.PageNumber - 1) * parameters.PageSize, parameters.PageSize, false, _cancellationToken)
		).Returns(Task.FromResult(attendances));
		mock.Setup(x => x.CountByConditionAsync(
			It.IsAny<Func<IQueryable<AttendanceEntity>, IQueryable<AttendanceEntity>>>(),
			false, _cancellationToken)
		).Returns(Task.FromResult(0));
		_repositoryServiceMock.Setup(x => x.AttendanceRepository)
			.Returns(mock.Object);

		ErrorOr<IPagedList<AttendanceResponse>> result = await _sut
			.GetPagedByParametersAsync(parameters, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Count.Should().Be(0);
			result.Value.MetaData.CurrentPage.Should().Be(1);
			result.Value.MetaData.HasNext.Should().BeFalse();
			result.Value.MetaData.HasPrevious.Should().BeFalse();
			result.Value.MetaData.PageSize.Should().Be(10);
			result.Value.MetaData.TotalCount.Should().Be(0);
			result.Value.MetaData.TotalPages.Should().Be(0);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
