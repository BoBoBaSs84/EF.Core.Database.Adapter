using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Contexts.Timekeeping;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.Timekeeping;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
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
		User newUser = EntityHelper.GetNewUser();
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();
		int dbUserId = repositoryManager.UserRepository.GetByCondition(x => x.UserName.Equals(newUser.UserName)).Id;

		IEnumerable<Attendance> dbAttendances = repositoryManager.AttendanceRepository.GetAllAttendances(dbUserId);

		dbAttendances.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public void GetAttendanceByUserIdAndCalendarIdTest()
	{
		User newUser = EntityHelper.GetNewUser();
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();
		int dbUserId = repositoryManager.UserRepository.GetByCondition(x => x.UserName.Equals(newUser.UserName)).Id;

		Attendance dbAttendance = repositoryManager.AttendanceRepository.GetAttendance(dbUserId, 1);

		dbAttendance.Should().NotBeNull();
	}

	[TestMethod]
	public void GetAttendanceByUserIdAndCalendarDateTest()
	{
		User newUser = EntityHelper.GetNewUser();
		repositoryManager.UserRepository.Create(newUser);
		repositoryManager.CommitChanges();
		int dbUserId = repositoryManager.UserRepository.GetByCondition(x => x.UserName.Equals(newUser.UserName)).Id;

		Attendance dbAttendance = repositoryManager.AttendanceRepository.GetAttendance(dbUserId, new DateTime(1900, 1, 1));

		dbAttendance.Should().NotBeNull();
	}
}
