using Application.Common.Interfaces;
using BaseTests.Helpers;
using Domain.Entities.Private;
using Domain.Enumerators;
using Domain.Extensions;
using FluentAssertions;
using static BaseTests.Constants;

namespace InfrastructureTests.Persistence.Repositories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class CalendarDayRepositoryTests : InfrastructureBaseTests
{
	private IUnitOfWork _unitOfWork = default!;
	private readonly int _year = DateTime.Today.Year;

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		DateTime date = RandomHelper.GetDateTime(_year);

		CalendarDay dbCalendarDay = await _unitOfWork.CalendarDayRepository.GetByDateAsync(date);

		AssertionHelper.AssertInScope(() =>
		{
			dbCalendarDay.Should().NotBeNull();
			dbCalendarDay.Year.Should().Be(date.Year);
			dbCalendarDay.Month.Should().Be(date.Month);
			dbCalendarDay.Day.Should().Be(date.Day);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDatesAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<DateTime> dates = new List<DateTime>()
		{
			RandomHelper.GetDateTime(_year),
			RandomHelper.GetDateTime(_year),
			RandomHelper.GetDateTime(_year)
		};

		IEnumerable<CalendarDay> dbCalendarDays = await _unitOfWork.CalendarDayRepository.GetByDateAsync(dates);

		AssertionHelper.AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.Should().HaveCount(dates.Count());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateRangeAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		DateTime startDate = new(_year, 1, 1);
		DateTime endDate = startDate.AddDays(14);

		IEnumerable<CalendarDay> dbCalendarDays = await _unitOfWork.CalendarDayRepository.GetByDateAsync(startDate, endDate);

		AssertionHelper.AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.First().Date.Should().Be(startDate);
			dbCalendarDays.Last().Date.Should().Be(endDate);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateTypeIdAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		int dayTypeId = (int)DayTypes.WEEKDAY;

		IEnumerable<CalendarDay> dbCalendarDays = await _unitOfWork.CalendarDayRepository.GetByDayTypeAsync(dayTypeId);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateTypeNameAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		string dayTypeName = DayTypes.WEEKDAY.GetName();

		IEnumerable<CalendarDay> dbCalendarDays = await _unitOfWork.CalendarDayRepository.GetByDayTypeAsync(dayTypeName);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByEndOfMonthAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		DateTime dateTime = DateTime.Now;

		IEnumerable<CalendarDay> dbCalendarDays = await _unitOfWork.CalendarDayRepository.GetByEndOfMonthAsync(dateTime);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}
}