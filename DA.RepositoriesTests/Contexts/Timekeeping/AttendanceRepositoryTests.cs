using DA.Domain.Models.Identity;
using DA.Domain.Models.Timekeeping;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.EntityHelper;

namespace DA.RepositoriesTests.Contexts.Timekeeping;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AttendanceRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public async Task GetAllAttendancesByUserIdTest()
	{
		User newUser = GetNewUser(attendanceSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		IEnumerable<Attendance> dbAttendances = await RepositoryManager.AttendanceRepository.GetAttendancesAsync(newUser.Id);

		dbAttendances.Should().NotBeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAttendanceByUserIdAndCalendarIdTest()
	{
		User newUser = GetNewUser(attendanceSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		Attendance dbAttendance = await RepositoryManager.AttendanceRepository.GetAttendanceAsync(newUser.Id, 1);

		dbAttendance.Should().NotBeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAttendanceByUserIdAndCalendarDateTest()
	{
		User newUser = GetNewUser(attendanceSeed: true);
		await RepositoryManager.UserRepository.CreateAsync(newUser);
		await RepositoryManager.CommitChangesAsync();

		Attendance dbAttendance = await RepositoryManager.AttendanceRepository.GetAttendanceAsync(newUser.Id, new DateTime(1900, 1, 1));

		dbAttendance.Should().NotBeNull();
	}
}
