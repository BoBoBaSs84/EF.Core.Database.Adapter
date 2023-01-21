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
	public async Task GetByDateTest()
	{
		DateTime dateTime = DateTime.Now;

		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByDateAsync(dateTime);

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

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetByDateAsync(dateTimes);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.Should().HaveCount(dateTimes.Count());
	}

	[TestMethod]
	public async Task GetByDateRangeTest()
	{
		DateTime mindateTime = DateTime.Now, maxDateTime = DateTime.Now.AddDays(14);

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetByDateAsync(mindateTime, maxDateTime);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Date.Should().Be(mindateTime);
		dbCalendarDays.Last().Date.Should().Be(maxDateTime);
	}

	[TestMethod]
	public async Task GetByDateTypeIdTest()
	{
		int dayTypeId = (int)Entities.Enumerators.DayType.WEEKENDDAY;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetByDayTypeAsync(dayTypeId);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public async Task GetByDateTypeNameTest()
	{
		string dayTypeName = Entities.Enumerators.DayType.WEEKDAY.GetName();

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetByDayTypeAsync(dayTypeName);

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}
}
