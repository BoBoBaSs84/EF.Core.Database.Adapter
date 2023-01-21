using Database.Adapter.Base.Tests;
using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CalendarDayRepositoryTests : BaseTest
{
	[TestMethod]
	public async Task GetByDateTest()
	{
		DateTime dateTime = DateTime.Now;

		CalendarDay dbCalendarDay = await RepositoryManager.CalendarRepository.GetByDateAsync(dateTime);

		dbCalendarDay.Should().NotBeNull();
	}

	[TestMethod]
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

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.Should().HaveCount(dateTimes.Count());
	}

	[TestMethod]
	public async Task GetByDateRangeTest()
	{
		DateTime mindateTime = DateTime.Now, maxDateTime = DateTime.Now.AddDays(14);

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByDateAsync(mindateTime, maxDateTime);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Date.Should().Be(mindateTime.ToSqlDate());
		dbCalendarDays.Last().Date.Should().Be(maxDateTime.ToSqlDate());
	}

	[TestMethod]
	public async Task GetByDateTypeIdTest()
	{
		int dayTypeId = (int)Entities.Enumerators.DayType.WEEKENDDAY;

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByDayTypeAsync(dayTypeId);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public async Task GetByDateTypeNameTest()
	{
		string dayTypeName = Entities.Enumerators.DayType.WEEKDAY.GetName();

		IEnumerable<CalendarDay> dbCalendarDays = await RepositoryManager.CalendarRepository.GetByDayTypeAsync(dayTypeName);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}
}
