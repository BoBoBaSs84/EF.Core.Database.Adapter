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

		internal const string Delete = "{id:guid}";
		internal const string GetByUserId = EmptySuffix;
		internal const string GetById = "{id:guid}";
		internal const string Post = EmptySuffix;
		internal const string Put = "{id:guid}";

		/// <summary>
		/// The transaction route.
		/// </summary>
		internal static class Transaction
		{
			internal const string Delete = "{accountId:guid}" + "/" + nameof(Transaction) + "/" + "{transactionId:guid}";
			internal const string GetAll = "{accountId:guid}" + "/" + nameof(Transaction);
			internal const string Get = "{accountId:guid}" + "/" + nameof(Transaction) + "/" + "{transactionId:guid}";
			internal const string Post = "{accountId:guid}" + "/" + nameof(Transaction);
			internal const string Put = "{accountId:guid}" + "/" + nameof(Transaction);
		}
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

		internal const string Delete = "{id:guid}";
		internal const string GetByUserId = EmptySuffix;
		internal const string GetByCardId = "{id:guid}";
		internal const string Post = "{id:guid}";
		internal const string Put = "{id:guid}";

		/// <summary>
		/// The transaction route.
		/// </summary>
		internal static class Transaction
		{
			internal const string Delete = "{cardId:guid}" + "/" + nameof(Transaction) + "/" + "{transactionId:guid}";
			internal const string GetAll = "{cardId:guid}" + "/" + nameof(Transaction);
			internal const string Get = "{cardId:guid}" + "/" + nameof(Transaction) + "/" + "{transactionId:guid}";
			internal const string Post = "{cardId:guid}" + "/" + nameof(Transaction);
			internal const string Put = "{cardId:guid}" + "/" + nameof(Transaction);
		}
	}

	/// <summary>
	/// The calendar route.
	/// </summary>
	internal static class Calendar
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Calendar);

		internal const string GetByDate = "{date}";
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
		/// The priority level type route
		/// </summary>
		internal static class PriorityLevelType
		{
			internal const string Get = nameof(PriorityLevelType);
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
	/// The todo list route.
	/// </summary>
	internal static class Todo
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Todo);

		internal const string DeleteList = "{listId:guid}";
		internal const string DeleteItem = "Items/{itemId:Guid}";
		internal const string GetAll = EmptySuffix;
		internal const string GetById = "{listId:guid}";
		internal const string PostList = EmptySuffix;
		internal const string PostItem = "{listId:guid}";
		internal const string PutList = "{listId:guid}";
		internal const string PutItem = "Items/{itemId:Guid}";
	}

	/// <summary>
	/// The attendance route.
	/// </summary>
	internal static class Attendance
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Attendance);

		internal const string DeleteById = "{id:guid}";
		internal const string DeleteByIds = "Multiple";
		internal const string GetByDate = "{date}";
		internal const string GetById = "{id:guid}";
		internal const string GetPagedByParameters = EmptySuffix;
		internal const string Post = EmptySuffix;
		internal const string PostMultiple = "Multiple";
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
