using DA.Models.Contexts.MasterData;
using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;

namespace DA.RepositoriesTests.Contexts.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CalendarDayRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public async Task GetByDateTest()
	{
		DateTime dateTime = DateTime.Now;

		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByDateAsync(dateTime);

		dbCalendarDay.Should().NotBeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDatesTest()
	{
		IEnumerable<DateTime> dateTimes = new List<DateTime>()
		{
			DateTime.Now,
			DateTime.Today.AddDays(5),
			DateTime.Now.AddDays(10),
			DateTime.Today.AddDays(15),
		};

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByDateAsync(dateTimes);

		AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.Should().HaveCount(dateTimes.Count());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateRangeTest()
	{
		DateTime mindateTime = DateTime.Now, maxDateTime = DateTime.Now.AddDays(14);

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByDateAsync(mindateTime, maxDateTime);

		AssertInScope(() =>
		{
			dbCalendarDays.Should().NotBeNullOrEmpty();
			dbCalendarDays.First().Date.Should().Be(mindateTime.ToSqlDate());
			dbCalendarDays.Last().Date.Should().Be(maxDateTime.ToSqlDate());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateTypeIdTest()
	{
		int dayTypeId = (int)Models.Enumerators.DayType.WEEKENDDAY;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByDayTypeAsync(dayTypeId);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByDateTypeNameTest()
	{
		string dayTypeName = Models.Enumerators.DayType.WEEKDAY.GetName();

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByDayTypeAsync(dayTypeName);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByEndOfMonthTest()
	{
		DateTime dateTime = DateTime.Now;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByEndOfMonthAsync(dateTime);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}
}
