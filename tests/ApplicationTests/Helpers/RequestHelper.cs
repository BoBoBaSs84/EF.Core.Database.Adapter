using System.Drawing;

using Application.Contracts.Requests.Attendance;
using Application.Contracts.Requests.Identity;
using Application.Contracts.Requests.Todo;

using BaseTests.Helpers;

using BB84.Extensions;

using Domain.Enumerators.Attendance;
using Domain.Enumerators.Todo;

namespace ApplicationTests.Helpers;

internal static class RequestHelper
{
	private const string UnitTestEmail = "UnitTest@UnitTest.net";

	internal static AttendanceCreateRequest GetAttendanceCreateRequest()
	{
		AttendanceCreateRequest request = new()
		{
			Date = DateTime.Today,
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(16, 0, 0),
			BreakTime = new(0, 45, 0)
		};

		return request;
	}

	internal static AttendanceUpdateRequest GetAttendanceUpdateRequest()
	{
		AttendanceUpdateRequest request = new()
		{
			Id = Guid.NewGuid(),
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(16, 0, 0),
			BreakTime = new(0, 45, 0)
		};

		return request;
	}

	internal static UserCreateRequest GetUserCreateRequest()
	{
		UserCreateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			Email = UnitTestEmail,
			UserName = RandomHelper.GetString(50),
			Password = RandomHelper.GetString(50),
		};
		return request;
	}

	internal static UserUpdateRequest GetUserUpdateRequest()
	{
		UserUpdateRequest request = new()
		{
			FirstName = RandomHelper.GetString(50),
			MiddleName = RandomHelper.GetString(50),
			LastName = RandomHelper.GetString(50),
			DateOfBirth = DateTime.Today,
			Email = UnitTestEmail,
			PhoneNumber = "+1234567890",
			Picture = [],
			Preferences = null
		};
		return request;
	}

	internal static ListCreateRequest GetListCreateRequest()
	{
		ListCreateRequest request = new()
		{
			Title = RandomHelper.GetString(25),
			Color = Color.White.ToRGBHexString()
		};

		return request;
	}

	internal static ListUpdateRequest GetListUpdateRequest()
	{
		ListUpdateRequest request = new()
		{
			Title = RandomHelper.GetString(25),
			Color = Color.Black.ToRGBHexString()
		};

		return request;
	}

	internal static ItemCreateRequest GetItemCreateRequest()
	{
		ItemCreateRequest request = new()
		{
			Title = RandomHelper.GetString(50),
			Priority = PriorityLevelType.NONE,
			Reminder = DateTime.Today,
			Note = RandomHelper.GetString(1024)
		};

		return request;
	}

	internal static ItemUpdateRequest GetItemUpdateRequest()
	{
		ItemUpdateRequest request = new()
		{
			Title = RandomHelper.GetString(50),
			Priority = PriorityLevelType.NONE,
			Reminder = DateTime.Today,
			Note = RandomHelper.GetString(1024),
			Done = true
		};

		return request;
	}
}
