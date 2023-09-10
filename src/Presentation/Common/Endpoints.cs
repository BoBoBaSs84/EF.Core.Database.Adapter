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
	/// The bank account route.
	/// </summary>
	internal static class Account
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Account);

		internal const string Delete = "{accountId:guid}";
		internal const string GetAll = EmptySuffix;
		internal const string GetById = "{accountId:guid}";
		internal const string GetByNumber = "{iban}";
		internal const string GetTransactions = "{accountId:guid}/Transactions";
		internal const string Post = EmptySuffix;
		internal const string Put = EmptySuffix;
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
	/// The bank card route.
	/// </summary>
	internal static class Card
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Card);

		internal const string Delete = "{cardId:guid}";
		internal const string GetAll = EmptySuffix;
		internal const string GetById = "{cardId:guid}";
		internal const string GetByNumber = "{pan}";
		internal const string GetTransactions = "{cardId:guid}/Transactions";
		internal const string Post = "{accountId:guid}";
		internal const string Put = EmptySuffix;
	}

	/// <summary>
	/// The calendar route.
	/// </summary>
	internal static class Calendar
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Calendar);

		internal const string GetByDate = "{date}";
		internal const string GetById = "{id:guid}";
		internal const string GetPagedByParameters = EmptySuffix;
		internal const string GetCurrent = "Current";
	}

	/// <summary>
	/// The enumerator route.
	/// </summary>
	internal static class Enumerator
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Enumerator);

		/// <summary>
		/// The attendance type route.
		/// </summary>
		internal static class AttendanceType
		{
			internal const string Get = nameof(AttendanceType);
		}

		/// <summary>
		/// The card type route.
		/// </summary>
		internal static class CardType
		{
			internal const string Get = nameof(CardType);
		}

		/// <summary>
		/// The role type route.
		/// </summary>
		internal static class RoleType
		{
			internal const string Get = nameof(RoleType);
		}

		/// <summary>
		/// The work day type route.
		/// </summary>
		internal static class WorkDayType
		{
			internal const string Get = nameof(WorkDayType);
		}
	}

	/// <summary>
	/// The attendance route.
	/// </summary>
	internal static class Attendance
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Attendance);

		internal const string Delete = "{calendarId:guid}";
		internal const string DeleteMultiple = "Multiple";
		internal const string GetByDate = "{date}";
		internal const string GetById = "{calendarId:guid}";
		internal const string GetPagedByParameters = EmptySuffix;
		internal const string Post = EmptySuffix;
		internal const string PostMultiple = $"Multiple";
		internal const string Put = EmptySuffix;
		internal const string PutMultiple = "Multiple";

		/// <summary>
		/// The settings route.
		/// </summary>
		internal static class Settings
		{
			internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Attendance) + "/" + nameof(Settings);

			internal const string Get = EmptySuffix;
			internal const string Post = EmptySuffix;
			internal const string Put = EmptySuffix;
		}
	}

	internal static class UserManagement
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(UserManagement);

		internal const string AddUserToRole = "Users/{userId:guid}/Roles/{roleId:guid}";
		internal const string Create = EmptySuffix;
		internal const string GetAll = EmptySuffix;
		internal const string GetByName = "{userName}";
		internal const string GetCurrent = "Current";
		internal const string RemoveUserToRole = "Users/{userId:guid}/Roles/{roleId:guid}";
		internal const string UpdateCurrent = "Current";
	}
}
