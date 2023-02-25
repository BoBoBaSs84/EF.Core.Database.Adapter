using DA.Domain.Models.Identity;
using DA.Domain.Models.Timekeeping;
using DA.Infrastructure.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;
using static DA.BaseTests.Helpers.EntityHelper;
using static DA.BaseTests.Helpers.RandomHelper;

namespace DA.RepositoriesTests.Contexts.Timekeeping;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AttendanceRepositoryTests : RepositoriesBaseTest
{
	private readonly IUserService _userService = GetRequiredService<IUserService>();

	[TestMethod, Owner(Bobo)]
	public async Task GetAllAttendancesByUserIdTest()
	{
		User newUser = GetNewUser(attendanceSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		IEnumerable<Attendance> dbAttendances = await RepositoryManager.AttendanceRepository.GetAttendancesAsync(newUser.Id);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAttendances.Should().NotBeNullOrEmpty();
			dbAttendances.Should().HaveCount(newUser.Attendances.Count);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAttendanceByUserIdAndCalendarIdTest()
	{
		User newUser = GetNewUser(attendanceSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Attendance dbAttendance = await RepositoryManager.AttendanceRepository.GetAttendanceAsync(newUser.Id, 1);

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAttendance.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAttendanceByUserIdAndCalendarDateTest()
	{
		User newUser = GetNewUser(attendanceSeed: true);
		string password = GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Attendance dbAttendance = await RepositoryManager.AttendanceRepository.GetAttendanceAsync(newUser.Id, new DateTime(1900, 1, 1));

		AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAttendance.Should().NotBeNull();
		});
	}
}
