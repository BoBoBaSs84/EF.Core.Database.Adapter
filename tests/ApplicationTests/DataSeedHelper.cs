using Application.Contracts.Requests.Identity;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Models.Common;
using Domain.Models.Identity;

using Infrastructure.Services;

namespace ApplicationTests;

/// <summary>
/// The data seed helper class.
/// </summary>
public static class DataSeedHelper
{
	public static IEnumerable<CalendarModel> SeedCalendar()
	{
		IRepositoryService repositoryService = ApplicationTestBase.GetService<IRepositoryService>();

		DateTime startDate = new(DateTime.Now.Year, 1, 1),
			endDate = new(DateTime.Now.Year, 12, 31);

		while (!startDate.Equals(endDate))
		{
			CalendarModel newCalendarEntry = new() { Date = startDate };
			repositoryService.CalendarRepository.CreateAsync(newCalendarEntry).Wait();
			startDate = startDate.AddDays(1);
		}

		repositoryService.CommitChangesAsync().Wait();

		return repositoryService.CalendarRepository.GetAllAsync().Result;
	}

	public static RoleModel SeedTestRole()
	{
		RoleService roleService = ApplicationTestBase.GetService<RoleService>();
		RoleModel testRole = new() { Id = Guid.NewGuid(), Name = "TestRole", Description = "Role for unit tests" };
		roleService.CreateAsync(testRole).Wait();
		return testRole;
	}

	public static UserModel SeedUser()
	{
		IMapper mapper = ApplicationTestBase.GetService<IMapper>();
		UserService userService = ApplicationTestBase.GetService<UserService>();
		IRepositoryService repositoryService = ApplicationTestBase.GetService<IRepositoryService>();
		IEnumerable<CalendarModel> calendar = repositoryService.CalendarRepository.GetAllAsync().Result;

		UserCreateRequest request = new UserCreateRequest().GetUserCreateRequest();
		UserModel newUser = mapper.Map<UserModel>(request);

		newUser.AccountUsers = ModelHelper.GetNewAccountUsers(newUser, 2, 5, 2, 5);
		newUser.Attendances = ModelHelper.GetNewAttendances(newUser, calendar.ToList(), 5);

		userService.CreateAsync(newUser, request.Password).Wait();
		userService.AddToRolesAsync(newUser, new[] { RoleType.USER.ToString() }).Wait();

		return newUser;
	}
}
