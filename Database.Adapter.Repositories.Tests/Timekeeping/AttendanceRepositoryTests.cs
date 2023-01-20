using Database.Adapter.Base.Tests;
using Database.Adapter.Base.Tests.Helpers;
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
		CreateTestUser();
		CreateTestAttendance();
		int userId = repositoryManager.UserRepository.GetByCondition(x => x.UserName == Constants.UnitTestUserName).Id;

		IEnumerable<Attendance> dbAttendances = repositoryManager.AttendanceRepository.GetAllAttendances(userId);

		dbAttendances.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public void GetAttendanceByUserIdAndCalendarIdTest()
	{
		CreateTestUser();
		CreateTestAttendance();
		int userId = repositoryManager.UserRepository.GetByCondition(x => x.UserName == Constants.UnitTestUserName).Id,
			calendarDayId = repositoryManager.CalendarRepository.GetByDate(DateTime.Today).Id;

		Attendance dbAttendance = repositoryManager.AttendanceRepository.GetAttendance(userId, calendarDayId);

		dbAttendance.Should().NotBeNull();
	}

	[TestMethod]
	public void GetAttendanceByUserIdAndCalendarDateTest()
	{
		CreateTestUser();
		CreateTestAttendance();
		int userId = repositoryManager.UserRepository.GetByCondition(x => x.UserName == Constants.UnitTestUserName).Id;
		DateTime calendarDate = DateTime.Today;

		Attendance dbAttendance = repositoryManager.AttendanceRepository.GetAttendance(userId, calendarDate);

		dbAttendance.Should().NotBeNull();
	}

	private void CreateTestUser()
	{
		Entities.Contexts.Authentication.User newUser = new()
		{
			FirstName = RandomHelper.GetString(64),
			LastName = RandomHelper.GetString(64),
			Email = "UnitTest@Test.org",
			NormalizedEmail = "UNITTEST@TEST.ORG",
			UserName = Constants.UnitTestUserName,
			NormalizedUserName = Constants.UnitTestUserName.ToUpper(),
		};
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();
	}

	private void CreateTestAttendance()
	{
		var user = repositoryManager.UserRepository.GetByCondition(x => x.UserName == Constants.UnitTestUserName);

		Attendance newAttendance = new()
		{
			CalendarDayId = repositoryManager.CalendarRepository.GetByDate(DateTime.Today).Id,
			UserId = user.Id,
			DayTypeId = (int)Entities.Enumerators.DayType.VACATION
		};

		repositoryManager.AttendanceRepository.Create(newAttendance);
		repositoryManager.CommitChanges();
	}
}
