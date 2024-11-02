using System.Linq.Expressions;

using Application.Contracts.Responses.Attendance;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Services.Attendance;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Attendance;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Attendance;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AttendanceServiceTests
{
	[TestMethod]
	[TestCategory("Method")]
	public async Task GetPagedByParametersShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		AttendanceParameters parameters = new();
		AttendanceService sut = CreateMockedInstance();

		ErrorOr<IPagedList<AttendanceResponse>> result = await sut.GetPagedByParameters(userId, parameters)
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
	[TestCategory("Method")]
	public async Task GetPagedByParametersShouldReturnResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		AttendanceParameters parameters = new();
		Mock<IAttendanceRepository> mock = new();
		mock.Setup(x => x.CountAsync(
			It.IsAny<Expression<Func<AttendanceModel, bool>>?>(),
			It.IsAny<Func<IQueryable<AttendanceModel>, IQueryable<AttendanceModel>>?>(), false, default)
		).Returns(Task.FromResult(10));
		AttendanceService sut = CreateMockedInstance(mock.Object);

		ErrorOr<IPagedList<AttendanceResponse>> result = await sut.GetPagedByParameters(userId, parameters)
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
			result.Value.MetaData.TotalCount.Should().Be(10);
			result.Value.MetaData.TotalPages.Should().Be(1);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
