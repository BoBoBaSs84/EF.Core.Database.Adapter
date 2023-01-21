using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CalendarDayRepositoryTests
{
	private TransactionScope transactionScope = default!;
	private IRepositoryManager repositoryManager = default!;

	[TestInitialize]
	public void TestInitialize()
	{
		transactionScope = new TransactionScope();
		repositoryManager = new RepositoryManager();
	}

	[TestCleanup]
	public void TestCleanup() => transactionScope.Dispose();

	[TestMethod]
	public void GetByDateTest()
	{
		DateTime dateTime = DateTime.Now;

		CalendarDay dbCalendarDay = repositoryManager.CalendarRepository.GetByDate(dateTime);

		dbCalendarDay.Should().NotBeNull();
	}

	[TestMethod]
	public void GetByDatesTest()
	{
		IEnumerable<DateTime> dateTimes = new List<DateTime>()
		{
			DateTime.Today,
			DateTime.Today.AddDays(5),
			DateTime.Today.AddDays(10),
			DateTime.Today.AddDays(15),
		};

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetByDate(dateTimes);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.Should().HaveCount(dateTimes.Count());
	}

	[TestMethod]
	public void GetByDateRangeTest()
	{
		DateTime mindateTime = DateTime.Today, maxDateTime = DateTime.Today.AddDays(14);

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetByDate(mindateTime, maxDateTime);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Date.Should().Be(mindateTime);
		dbCalendarDays.Last().Date.Should().Be(maxDateTime);
	}

	[TestMethod]
	public void GetByDateTypeIdTest()
	{
		int dayTypeId = (int)Entities.Enumerators.DayType.WEEKENDDAY;

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetByDayType(dayTypeId);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public void GetByDateTypeNameTest()
	{
		string dayTypeName = Entities.Enumerators.DayType.WEEKDAY.GetName();

		IEnumerable<CalendarDay> dbCalendarDays = repositoryManager.CalendarRepository.GetByDayType(dayTypeName);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}
}
