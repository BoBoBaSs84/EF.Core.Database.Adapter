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
	private ICalendarService _calendarDayService = default!;

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetByDateSuccessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarService>();

		ErrorOr<CalendarResponse> result = await _calendarDayService.Get(DateTime.Now);

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
		_calendarDayService = GetRequiredService<ICalendarService>();
		DateTime dateTime = DateTime.Now.AddYears(50);

		ErrorOr<CalendarResponse> result = await _calendarDayService.Get(dateTime);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Value.Should().BeNull();
			result.Errors.First().Should().Be(CalendarServiceErrors.GetByDateNotFound(dateTime));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetByIdSuccessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarService>();
		Guid calendarDayId = Guid.NewGuid();

		ErrorOr<CalendarResponse> result = await _calendarDayService.Get(calendarDayId);

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
		_calendarDayService = GetRequiredService<ICalendarService>();
		Guid calendarDayId = Guid.NewGuid();

		ErrorOr<CalendarResponse> result = await _calendarDayService.Get(calendarDayId);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Value.Should().BeNull();
			result.Errors.First().Should().Be(CalendarServiceErrors.GetByIdNotFound(calendarDayId));
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetPagedByParametersSucessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarService>();

		CalendarParameters parameters = new() { Year = DateTime.Now.Year, Month = DateTime.Now.Month };

		ErrorOr<IPagedList<CalendarResponse>> result = await _calendarDayService.Get(parameters);

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
		_calendarDayService = GetRequiredService<ICalendarService>();

		CalendarParameters parameters = new() { Year = DateTime.Now.AddYears(50).Year, Month = DateTime.Now.AddYears(50).Month };

		ErrorOr<IPagedList<CalendarResponse>> result = await _calendarDayService.Get(parameters);

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().NotBeEmpty();
			result.Value.Should().BeNullOrEmpty();
			result.Errors.First().Should().Be(CalendarServiceErrors.GetPagedByParametersNotFound);
		});
	}

	[TestMethod, Owner(TC.Bobo)]
	public async Task GetCurrentDateSuccessTest()
	{
		_calendarDayService = GetRequiredService<ICalendarService>();

		ErrorOr<CalendarResponse> result = await _calendarDayService.Get();

		AH.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Date.Should().Be(DateTime.Today);
		});
	}
}