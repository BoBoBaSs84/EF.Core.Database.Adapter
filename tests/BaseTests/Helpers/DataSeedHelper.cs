using Application.Contracts.Requests.Identity;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using Domain.Models.Identity;
using Domain.Enumerators;
using Domain.Models.Common;

using Tynamix.ObjectFiller;

namespace BaseTests.Helpers;

/// <summary>
/// The data seed helper class.
/// </summary>
internal static class DataSeedHelper
{
	public static void SeedCalendar()
	{
		IRepositoryService repositoryService = TestBase.GetRequiredService<IRepositoryService>();

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
		IMapper mapper = TestBase.GetRequiredService<IMapper>();
		IUserService userService = TestBase.GetRequiredService<IUserService>();
		IRepositoryService repositoryService = TestBase.GetRequiredService<IRepositoryService>();
		IEnumerable<CalendarModel> calendar = repositoryService.CalendarRepository.GetAllAsync(true, false).Result;

		for (int i = 0; i < 10; i++)
		{
			UserCreateRequest request = new UserCreateRequest().GetUserCreateRequest();
			UserModel newUser = mapper.Map<UserModel>(request);
			TestBase.Users.Add(new() { UserName = request.UserName });

			newUser.AccountUsers = ModelHelper.GetNewAccountUsers(newUser);
			newUser.Attendances = ModelHelper.GetNewAttendances(newUser, calendar.ToList());

			userService.CreateAsync(newUser, request.Password).Wait();
			userService.AddToRolesAsync(newUser, new[] { RoleType.USER.ToString() }).Wait();
		}
	}
}
