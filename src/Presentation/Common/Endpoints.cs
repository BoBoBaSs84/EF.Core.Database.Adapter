namespace Presentation.Common;

/// <summary>
/// Static class for the api endpoints.
/// </summary>
internal static class Endpoints
{
	private const string SiteRoot = "api";

	internal const string BaseUri = SiteRoot;

	internal const string EmptySuffix = "";

	/// <summary>
	/// The account route.
	/// </summary>
	internal static class Account
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Account);

		internal const string GetAll = EmptySuffix;
		internal const string GetByIban = "{iban}";
	}

	/// <summary>
	/// The authentication route.
	/// </summary>
	internal static class Authentication
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Authentication);

		internal const string Authenticate = EmptySuffix;
	}

	/// <summary>
	/// The calendar day route.
	/// </summary>
	internal static class CalendarDay
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(CalendarDay);

		internal const string GetByDate = "{date}";
		internal const string GetById = "{id:int}";
		internal const string GetPagedByParameters = EmptySuffix;
	}

	/// <summary>
	/// The day type route.
	/// </summary>
	internal static class DayType
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(DayType);

		internal const string GetById = "{id:int}";
		internal const string GetByName = "{name}";
		internal const string GetPagedByParameters = EmptySuffix;
	}

	/// <summary>
	/// The card type route.
	/// </summary>
	internal static class CardType
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(CardType);

		internal const string GetById = "{id:int}";
		internal const string GetByName = "{name}";
		internal const string GetPagedByParameters = EmptySuffix;
	}

	/// <summary>
	/// The attendance route.
	/// </summary>
	internal static class Attendance
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Attendance);

		internal const string Delete = "{calendarDayId:int}";
		internal const string DeleteMultiple = "Multiple";
		internal const string GetByDate = "{date}";
		internal const string GetById = "{calendarDayId:int}";
		internal const string GetPagedByParameters = EmptySuffix;
		internal const string Post = EmptySuffix;
		internal const string PostMultiple = $"Multiple";
		internal const string Put = EmptySuffix;
		internal const string PutMultiple = "Multiple";
	}

	internal static class UserManagement
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(UserManagement);

		internal const string AddUserToRole = "User/{userId:int}/Roles/{roleName}";
		internal const string Create = EmptySuffix;
		internal const string GetAll = EmptySuffix;
		internal const string GetByName = "{userName}";
		internal const string GetCurrent = "Current";
		internal const string RemoveUserToRole = "User/{userId:int}/Roles/{roleName}";
		internal const string UpdateCurrent = "Current";
	}
}
