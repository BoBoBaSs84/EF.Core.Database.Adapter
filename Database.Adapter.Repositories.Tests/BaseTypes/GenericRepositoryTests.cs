using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using static Database.Adapter.Entities.Enumerators.DayType;

namespace Database.Adapter.Repositories.Tests.BaseTypes;

[TestClass()]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class GenericRepositoryTests
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
	public async Task CreateTest()
	{
		CalendarDay newCalendarDay = GetCalendarDay();

		await repositoryManager.CalendarRepository.CreateAsync(newCalendarDay);
		await repositoryManager.CommitChangesAsync();
		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Date.Equals(newCalendarDay.Date));

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Date.Should().Be(newCalendarDay.Date);
	}

	[TestMethod]
	public async Task CreateManyTest()
	{
		IEnumerable<CalendarDay> calendarDays = GetCalendarDays(2);

		await repositoryManager.CalendarRepository.CreateAsync(calendarDays);
		int commit = await repositoryManager.CommitChangesAsync();

		commit.Should().Be(calendarDays.Count());
	}

	[TestMethod]
	public async Task DeleteByEntityTest()
	{
		int calendarDayId = 10;

		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);
		await repositoryManager.CalendarRepository.DeleteAsync(dbCalendarDay);
		await repositoryManager.CommitChangesAsync();
		dbCalendarDay = await repositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod]
	public async Task DeleteByIdTest()
	{
		int calendarDayId = 10;

		await repositoryManager.CalendarRepository.DeleteAsync(calendarDayId);
		await repositoryManager.CommitChangesAsync();
		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod]
	public async Task DeleteByExpressionTest()
	{
		int calendarDayId = 10;

		await repositoryManager.CalendarRepository.DeleteAsync(x => x.Id.Equals(calendarDayId));
		await repositoryManager.CommitChangesAsync();
		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().BeNull();
	}

	[TestMethod]
	public async Task DeleteManyTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));
		await repositoryManager.CalendarRepository.DeleteAsync(dbCalendarDays);
		await repositoryManager.CommitChangesAsync();
		dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));

		dbCalendarDays.Should().BeEmpty();
	}

	[TestMethod]
	public async Task GetAllTest()
	{
		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetAllAsync();

		dbCalendarDays.Should().NotBeNullOrEmpty();
	}

	[TestMethod()]
	public async Task GetManyByConditionTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Year.Should().Be(calendarYear);
		dbCalendarDays.First().Month.Should().Be(calendarMonth);
	}

	[TestMethod]
	public async Task GetManyByConditionWithOrderByDescendingTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderByDescending(x => x.Date));

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Day.Should().Be(31);
	}

	[TestMethod]
	public async Task GetManyByConditionWithSkippingFirstTenTakingNextTenTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 10,
			skip: 10);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.Should().HaveCount(10);
		dbCalendarDays.First().Day.Should().Be(11);
		dbCalendarDays.Last().Day.Should().Be(20);
	}

	[TestMethod]
	public async Task GetManyByConditionWithTakingTwelveTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Date),
			top: 12);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.Should().HaveCount(12);
		dbCalendarDays.First().Day.Should().Be(1);
		dbCalendarDays.Last().Day.Should().Be(12);
	}

	[TestMethod]
	public async Task GetManyByConditionWithIncludeTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			orderBy: x => x.OrderBy(x => x.Id),
			top: 1,
			includeProperties: new[] { nameof(CalendarDay.DayType) });

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().DayType.Should().NotBeNull();
	}

	[TestMethod]
	public async Task GetByIdTest()
	{
		int calendarDayId = 1;

		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Id.Should().Be(calendarDayId);
	}

	[TestMethod]
	public async Task GetByConditionTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12,
			calenderDay = 6;

		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth) && x.Day.Equals(calenderDay));

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Year.Should().Be(calendarYear);
		dbCalendarDay.Month.Should().Be(calendarMonth);
		dbCalendarDay.Day.Should().Be(calenderDay);
	}

	[TestMethod]
	public async Task GetByConditionWithInclude()
	{
		int calendarYear = 2020,
			calendarMonth = 12,
			calenderDay = 24;

		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth) && x.Day.Equals(calenderDay),
			includeProperties: new[] { nameof(CalendarDay.DayType) }
			);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Year.Should().Be(calendarYear);
		dbCalendarDay.Month.Should().Be(calendarMonth);
		dbCalendarDay.Day.Should().Be(calenderDay);
		dbCalendarDay.DayType.Should().NotBeNull();
	}

	[TestMethod]
	public async Task UpdateTest()
	{
		int calendarDayId = 12;
		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Id.Equals(calendarDayId)
			);

		dbCalendarDay.Date = GetDateTime();
		await repositoryManager.CalendarRepository.UpdateAsync(dbCalendarDay);
		await repositoryManager.CommitChangesAsync();
		dbCalendarDay = await repositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.Date.Should().Be(GetDateTime());
	}

	[TestMethod]
	public async Task UpdateManyTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth));
		foreach (CalendarDay dbCalendarDay in dbCalendarDays.Where(x => x.Day.Equals(25) || x.Day.Equals(26)))
			dbCalendarDay.DayTypeId = (int)ABSENCE;
		await repositoryManager.CalendarRepository.UpdateAsync(dbCalendarDays);
		int commit = await repositoryManager.CommitChangesAsync();

		commit.Should().Be(31);
	}

	[TestMethod]
	public async Task UpdateTrackChangesTest()
	{
		int calendarDayId = 12;

		CalendarDay dbCalendarDay = await repositoryManager.CalendarRepository.GetByConditionAsync(
			expression: x => x.Id.Equals(calendarDayId), trackChanges: true);
		dbCalendarDay.DayTypeId = (int)SICKNESS;
		await repositoryManager.CommitChangesAsync();
		dbCalendarDay = await repositoryManager.CalendarRepository.GetByIdAsync(calendarDayId);

		dbCalendarDay.Should().NotBeNull();
		dbCalendarDay.DayTypeId.Should().Be((int)SICKNESS);
	}

	[TestMethod]
	public async Task UpdateManyTrackChangesTest()
	{
		int calendarYear = 2020,
			calendarMonth = 12;

		IEnumerable<CalendarDay> dbCalendarDays = await repositoryManager.CalendarRepository.GetManyByConditionAsync(
			expression: x => x.Year.Equals(calendarYear) && x.Month.Equals(calendarMonth),
			trackChanges: true);
		foreach (CalendarDay dbCalendarDay in dbCalendarDays.Where(x => x.Day.Equals(25) || x.Day.Equals(26)))
			dbCalendarDay.DayTypeId = (int)BUISNESSTRIP;
		int commit = await repositoryManager.CommitChangesAsync();

		commit.Should().Be(2);
	}

	private static DateTime GetDateTime(int dayToAdd = 0)
	{
		DateTime newDateTime = new(2199, 1, 1);
		return newDateTime.AddDays(dayToAdd);
	}

	private static CalendarDay GetCalendarDay(int dayToAdd = 0) => new()
	{
		Date = GetDateTime(dayToAdd),
		DayTypeId = GetDateTime(dayToAdd).DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday ? (int)WEEKENDDAY : (int)WEEKDAY
	};

	private static IEnumerable<CalendarDay> GetCalendarDays(int daysToAdd = 0)
	{
		List<CalendarDay> calendarDays = new();
		for (int i = 0; i <= daysToAdd; i++)
			calendarDays.Add(GetCalendarDay(i));
		return calendarDays;
	}
}