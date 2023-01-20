using Database.Adapter.Entities.Contexts.Timekeeping;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.Timekeeping;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AttendanceRepositoryTests
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
	public void GetAllAttendancesByUserIdTest()
	{
		int userId = 1;

		IEnumerable<Attendance> dbAttendances = repositoryManager.AttendanceRepository.GetAllAttendances(userId);

		dbAttendances.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public void GetAttendanceByUserIdAndCalendarIdTest()
	{
		int userId = 1, calendarDayId = 1;

		Attendance dbAttendance = repositoryManager.AttendanceRepository.GetAttendance(userId, calendarDayId);

		dbAttendance.Should().NotBeNull();
	}

	[TestMethod]
	public void GetAttendanceByUserIdAndCalendarDateTest()
	{
		int userId = 1;
		DateTime calendarDate = DateTime.Today;

		Attendance dbAttendance = repositoryManager.AttendanceRepository.GetAttendance(userId, calendarDate);

		dbAttendance.Should().NotBeNull();
	}
}
