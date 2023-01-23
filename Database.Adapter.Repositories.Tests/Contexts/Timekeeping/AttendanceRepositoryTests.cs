using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.Authentication;
using Database.Adapter.Entities.Contexts.Timekeeping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Tests.Contexts.Timekeeping;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AttendanceRepositoryTests : RepositoriesBaseTest
{
	[TestMethod]
	public async Task GetAllAttendancesByUserIdTest()
	{
		User newUser = EntityHelper.GetNewUser(attendanceSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		IEnumerable<Attendance> dbAttendances = await RepositoryManager.AttendanceRepository.GetAttendancesAsync(newUser.Id);

		dbAttendances.Should().NotBeNullOrEmpty();
	}

	[TestMethod]
	public async Task GetAttendanceByUserIdAndCalendarIdTest()
	{
		User newUser = EntityHelper.GetNewUser(attendanceSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		Attendance dbAttendance = await RepositoryManager.AttendanceRepository.GetAttendanceAsync(newUser.Id, 1);

		dbAttendance.Should().NotBeNull();
	}

	[TestMethod]
	public async Task GetAttendanceByUserIdAndCalendarDateTest()
	{
		User newUser = EntityHelper.GetNewUser(attendanceSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		Attendance dbAttendance = await RepositoryManager.AttendanceRepository.GetAttendanceAsync(newUser.Id, new DateTime(1900, 1, 1));

		dbAttendance.Should().NotBeNull();
	}
}
