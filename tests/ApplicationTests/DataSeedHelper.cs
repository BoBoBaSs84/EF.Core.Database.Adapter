using Application.Contracts.Requests.Identity;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Models.Common;
using Domain.Models.Identity;

namespace ApplicationTests;

/// <summary>
/// The data seed helper class.
/// </summary>
public static class DataSeedHelper
{
	public static void SeedCalendar()
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
	}

	public static void SeedUsers()
	{
		IMapper mapper = ApplicationTestBase.GetService<IMapper>();
		IUserService userService = ApplicationTestBase.GetService<IUserService>();
		IRoleService roleService = ApplicationTestBase.GetService<IRoleService>();
		IRepositoryService repositoryService = ApplicationTestBase.GetService<IRepositoryService>();
		IEnumerable<CalendarModel> calendar = repositoryService.CalendarRepository.GetAllAsync(true, false).Result;

		RoleModel testRole = new() { Id = Guid.NewGuid(), Name = "TestRole", Description = "Role for unit tests" };
		roleService.CreateAsync(testRole).Wait();
		ApplicationTestBase.Roles.Add(testRole);

		for (int i = 0; i < 10; i++)
		{
			UserCreateRequest request = new UserCreateRequest().GetUserCreateRequest();
			UserModel newUser = mapper.Map<UserModel>(request);

			newUser.AccountUsers = ModelHelper.GetNewAccountUsers(newUser);
			newUser.Attendances = ModelHelper.GetNewAttendances(newUser, calendar.ToList());

			userService.CreateAsync(newUser, request.Password).Wait();
			userService.AddToRolesAsync(newUser, new[] { RoleType.USER.ToString() }).Wait();

			ApplicationTestBase.Users.Add(newUser);
		}
	}
}
