using Application.Common.Interfaces;
using Application.Common.Interfaces.Identity;
using BaseTests.Helpers;
using Domain.Entities.Identity;
using Domain.Entities.Private;
using FluentAssertions;
using InfrastructureTests.Helpers;
using Microsoft.AspNetCore.Identity;
using static BaseTests.Constants;

namespace InfrastructureTests.Persistence.Repositories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class AttendanceRepositoryTests : InfrastructureBaseTests
{
	private readonly int _year = DateTime.Today.Year;
	private IUnitOfWork _unitOfWork = default!;
	private IUserService _userService = default!;

	[TestMethod, Owner(Bobo)]
	public async Task GetAttendancesByUserIdAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(attendanceSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);
		IdentityResult result = await _userService.CreateAsync(newUser, password);
		IEnumerable<Attendance> dbAttendances = await _unitOfWork.AttendanceRepository.GetAttendancesAsync(newUser.Id);

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAttendances.Should().NotBeNullOrEmpty();
			dbAttendances.Should().HaveCount(newUser.Attendances.Count);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAttendanceByUserIdAndCalendarIdAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(attendanceSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Attendance dbAttendance = await _unitOfWork.AttendanceRepository.GetAttendanceAsync(newUser.Id, 1);

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAttendance.Should().NotBeNull();
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAttendanceByUserIdAndCalendarDateAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();
		_userService = GetRequiredService<IUserService>();

		User newUser = EntityHelper.GetNewUser(attendanceSeed: true);
		string password = RandomHelper.GetString(32, WildCardChars);

		IdentityResult result = await _userService.CreateAsync(newUser, password);
		Attendance dbAttendance = await _unitOfWork.AttendanceRepository.GetAttendanceAsync(newUser.Id, new DateTime(_year, 1, 1));

		AssertionHelper.AssertInScope(() =>
		{
			result.Succeeded.Should().BeTrue();
			result.Errors.Should().BeEmpty();
			dbAttendance.Should().NotBeNull();
		});
	}
}