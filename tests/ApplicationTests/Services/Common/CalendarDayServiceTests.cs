using Application.Contracts.Responses.Common;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Common;
using Application.Interfaces.Infrastructure.Logging;
using Application.Services.Common;

using AutoMapper;

using Domain.Errors;

using FluentAssertions;

using Moq;

namespace ApplicationTests.Services.Common;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit tests.")]
public sealed class CalendarDayServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<IDateTimeService> _dateTimeServiceMock = default!;
	private Mock<ILoggerService<CalendarDayService>> _loggerServiceMock = default!;

	[TestMethod, TestCategory("Methods")]
	public void GetByDateShouldReturnResponseWhenSuccessfully()
	{
		CalendarDayService sut = CreateMockedInstance();
		DateTime date = DateTime.Today;

		ErrorOr<CalendarResponse> result = sut.GetByDate(date);

		result.Should().NotBeNull();
		result.IsError.Should().BeFalse();
		result.Value.Date.Should().Be(date.Date);
	}

	[TestMethod, TestCategory("Methods")]
	public void GetByDateShouldReturnFailedResponseWhenExcpetionGetThrown()
	{
		CalendarDayService sut = CreateMockedInstance();
		DateTime date = DateTime.MinValue;

		ErrorOr<CalendarResponse> result = sut.GetByDate(date);

		result.Should().NotBeNull();
		result.IsError.Should().BeTrue();
		result.Errors.First().Should().Be(CalendarServiceErrors.GetByDateFailed);
	}

	[TestMethod, TestCategory("Methods")]
	public void GetPagedByParametersShouldReturnResponseWhenSuccessfully()
	{
		CalendarDayService sut = CreateMockedInstance();
		CalendarParameters parameters = new() { Year = 2020, PageSize = 100 };

		ErrorOr<IPagedList<CalendarResponse>> result = sut.GetPagedByParameters(parameters);

		result.Should().NotBeNull();
		result.IsError.Should().BeFalse();
		result.Value.Count.Should().Be(100);
		result.Value.MetaData.PageSize.Should().Be(100);
		result.Value.MetaData.CurrentPage.Should().Be(1);
		result.Value.MetaData.TotalPages.Should().Be(4);
	}

	[TestMethod, TestCategory("Methods")]
	public void GetPagedByParametersShouldReturnFailedResponseWhenExcpetionGetThrown()
	{
		CalendarDayService sut = CreateMockedInstance();

		ErrorOr<IPagedList<CalendarResponse>> result = sut.GetPagedByParameters(null!);

		result.Should().NotBeNull();
		result.IsError.Should().BeTrue();
		result.Errors.First().Should().Be(CalendarServiceErrors.GetPagedByParametersFailed);
	}

	[TestMethod, TestCategory("Methods")]
	public void GetCurrentShouldReturnResponseWhenSuccessfully()
	{
		DateTime today = DateTime.Today;
		CalendarDayService sut = CreateMockedInstance();
		_dateTimeServiceMock.Setup(x => x.Today).Returns(today);

		ErrorOr<CalendarResponse> result = sut.GetCurrent();

		result.Should().NotBeNull();
		result.IsError.Should().BeFalse();
		result.Value.Date.Should().Be(today);
	}

	[TestMethod, TestCategory("Methods")]
	public void GetCurrentShouldReturnFailedResponseWhenExcpetionGetThrown()
	{
		CalendarDayService sut = CreateMockedInstance();
		_dateTimeServiceMock.Setup(x => x.Today).Returns(DateTime.MinValue);

		ErrorOr<CalendarResponse> result = sut.GetCurrent();

		result.Should().NotBeNull();
		result.IsError.Should().BeTrue();
		result.Errors.First().Should().Be(CalendarServiceErrors.GetCurrentDateFailed);
	}

	private CalendarDayService CreateMockedInstance()
	{
		_dateTimeServiceMock = new();
		_loggerServiceMock = new();

		return new(_dateTimeServiceMock.Object, _loggerServiceMock.Object, _mapper);
	}
}