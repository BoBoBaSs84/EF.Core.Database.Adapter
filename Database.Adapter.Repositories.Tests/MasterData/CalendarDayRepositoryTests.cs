using Database.Adapter.Entities.Contexts.Application.MasterData;
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
	public void GetByDateRangeTest()
	{
		DateTime mindateTime = DateTime.Today, maxDateTime = DateTime.Today.AddDays(14);

		IEnumerable<CalendarDay> dbCalendarDays =
			repositoryManager.CalendarRepository.GetByDateRange(mindateTime, maxDateTime);

		dbCalendarDays.Should().NotBeNullOrEmpty();
		dbCalendarDays.First().Date.Should().Be(mindateTime);
		dbCalendarDays.Last().Date.Should().Be(maxDateTime);
	}
}
