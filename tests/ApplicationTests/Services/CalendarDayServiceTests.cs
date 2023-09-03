using Application.Contracts.Responses.Common;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;

using Domain.Errors;

using FluentAssertions;

using AH = BaseTests.Helpers.AssertionHelper;
using TC = BaseTests.Constants.TestConstants;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public class CalendarDayServiceTests : ApplicationBaseTests
{
	private ICalendarDayService _calendarDayService = default!;

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetByDateSuccessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarDayService>();

		ErrorOr<CalendarDayResponse> result = await _calendarDayService.Get(DateTime.Now);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Date.Should().Be(DateTime.Today);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetByDateNotFoundTest()
	{
		_calendarDayService = GetRequiredService<ICalendarDayService>();
		DateTime dateTime = DateTime.Now.AddYears(50);

		ErrorOr<CalendarDayResponse> result = await _calendarDayService.Get(dateTime);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Value.Should().BeNull();
			result.Errors.First().Should().Be(CalendarDayServiceErrors.GetByDateNotFound(dateTime));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetByIdSuccessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarDayService>();
		int calendarDayId = 1;

		ErrorOr<CalendarDayResponse> result = await _calendarDayService.Get(calendarDayId);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Id.Should().Be(calendarDayId);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetByIdNotFoundTest()
	{
		_calendarDayService = GetRequiredService<ICalendarDayService>();
		int calendarDayId = int.MaxValue;

		ErrorOr<CalendarDayResponse> result = await _calendarDayService.Get(calendarDayId);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Value.Should().BeNull();
			result.Errors.First().Should().Be(CalendarDayServiceErrors.GetByIdNotFound(calendarDayId));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetPagedByParametersSucessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarDayService>();

		CalendarDayParameters parameters = new() { Year = DateTime.Now.Year, Month = DateTime.Now.Month };

		ErrorOr<IPagedList<CalendarDayResponse>> result = await _calendarDayService.Get(parameters);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNullOrEmpty();
			result.Value.First().Year.Should().Be(DateTime.Now.Year);
			result.Value.First().Month.Should().Be(DateTime.Now.Month);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetPagedByParametersNotFoundTest()
	{
		_calendarDayService = GetRequiredService<ICalendarDayService>();

		CalendarDayParameters parameters = new() { Year = DateTime.Now.AddYears(50).Year, Month = DateTime.Now.AddYears(50).Month };

		ErrorOr<IPagedList<CalendarDayResponse>> result = await _calendarDayService.Get(parameters);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Value.Should().BeNullOrEmpty();
			result.Errors.First().Should().Be(CalendarDayServiceErrors.GetPagedByParametersNotFound);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetCurrentDateSuccessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarDayService>();

		ErrorOr<CalendarDayResponse> result = await _calendarDayService.Get();

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Date.Should().Be(DateTime.Today);
		});
	}
}