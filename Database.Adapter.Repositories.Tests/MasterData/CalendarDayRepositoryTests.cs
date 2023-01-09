using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
public class CalendarDayRepositoryTests
{
	private TransactionScope transactionScope = default!;
	private IMasterDataRepository masterDataRepository = default!;

	[TestInitialize]
	public void TestInitialize()
	{
		transactionScope = new TransactionScope();
		masterDataRepository = new MasterDataRepository();
	}

	[TestCleanup]
	public void TestCleanup()
	{
		transactionScope.Dispose();
	}

	[TestMethod]
	public void GetAllTest()
	{
		IQueryable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetAll();

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
	}

	[TestMethod]
	public void GetByYearTest()
	{
		IQueryable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetByYear(DateTime.Now.Year);

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
	}

	[TestMethod]
	public void GetWithinDateRangeTest()
	{
		DateTime startDate = DateTime.Now.AddDays(-14), endDate = DateTime.Now;
		IQueryable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetWithinDateRange(startDate, endDate);

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
	}

	[TestMethod]
	public void GetByConditionTest()
	{
		DateTime newDateTime = new(2020, 1, 1);
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetByCondition(x => x.Date.Equals(newDateTime));

		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Date, newDateTime);
	}

	[TestMethod]
	public void GetManyByConditionTest()
	{
		int calendarYear = 2020, calendarMonth = 12;
		IQueryable<CalendarDay> dbCalendarDays = masterDataRepository.CalendarRepository.GetManyByCondition(x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));

		Assert.IsNotNull(dbCalendarDays);
		Assert.IsTrue(dbCalendarDays.Any());
	}

	[TestMethod]
	public void GetByIdTest()
	{
		int calendarDayId = 1;
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetById(calendarDayId);

		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Id, calendarDayId);
	}

	[TestMethod]
	public void GetByDateTest()
	{
		DateTime newDateTime = new(2020, 1, 1);
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetByDate(newDateTime);
		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Date, newDateTime);
	}

	[TestMethod]
	public void CreateTest()
	{
		DateTime newDateTime = new(2099, 1, 1);
		CalendarDay newCalendarDay = new()
		{
			Date = newDateTime,
			DayTypeId = (newDateTime.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday) ? 2 : 1
		};

		masterDataRepository.CalendarRepository.Create(newCalendarDay);
		int commit = masterDataRepository.CommitChanges();
		CalendarDay dbCalendarDay = masterDataRepository.CalendarRepository.GetByDate(newDateTime);

		Assert.AreEqual(1, commit);
		Assert.IsNotNull(dbCalendarDay);
		Assert.AreEqual(dbCalendarDay.Date, newDateTime);
	}
}
