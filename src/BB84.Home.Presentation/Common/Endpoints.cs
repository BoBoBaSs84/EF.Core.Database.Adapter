﻿namespace BB84.Home.Presentation.Common;

/// <summary>
/// Static class for the api endpoints.
/// </summary>
internal static class Endpoints
{
	private const string SiteRoot = "";

	internal const string BaseUri = SiteRoot;

	internal const string EmptySuffix = "";

	/// <summary>
	/// The bank account route.
	/// </summary>
	internal static class Account
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Account);

		internal const string DeleteById = "{id:guid}";
		internal const string GetByUserId = EmptySuffix;
		internal const string GetById = "{id:guid}";
		internal const string Post = EmptySuffix;
		internal const string Put = "{id:guid}";

		/// <summary>
		/// The transaction route.
		/// </summary>
		internal static class Transaction
		{
			internal const string DeleteByAccountId = "{accountId:guid}" + "/" + nameof(Transaction) + "/" + "{id:guid}";
			internal const string GetPagedByAccountId = "{accountId:guid}" + "/" + nameof(Transaction);
			internal const string GetByAccountId = "{accountId:guid}" + "/" + nameof(Transaction) + "/" + "{id:guid}";
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

		/// <summary>
		/// The token route.
		/// </summary>
		internal static class Token
		{
			internal const string Refresh = $"{nameof(Token)}";
			internal const string Revoke = $"{nameof(Token)}";
		}
	}

	/// <summary>
	/// The bank card route.
	/// </summary>
	internal static class Card
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Card);

		internal const string DeleteById = "{id:guid}";
		internal const string GetByUserId = EmptySuffix;
		internal const string GetByCardId = "{id:guid}";
		internal const string Post = "{id:guid}";
		internal const string Put = "{id:guid}";

		/// <summary>
		/// The transaction route.
		/// </summary>
		internal static class Transaction
		{
			internal const string DeleteByCardId = "{cardId:guid}" + "/" + nameof(Transaction) + "/" + "{id:guid}";
			internal const string GetPagedByCardId = "{cardId:guid}" + "/" + nameof(Transaction);
			internal const string GetByCardId = "{cardId:guid}" + "/" + nameof(Transaction) + "/" + "{id:guid}";
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
	/// The document route.
	/// </summary>
	internal static class Document
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Document);

		internal const string DeleteById = "{id:guid}";
		internal const string DeleteByIds = "Multiple";
		internal const string GetById = "{id:guid}";
		internal const string GetPagedByParameters = EmptySuffix;
		internal const string Post = EmptySuffix;
		internal const string PostMultiple = "Multiple";
		internal const string Put = EmptySuffix;
		internal const string PutMultiple = "Multiple";
	}

	/// <summary>
	/// The enumerator route.
	/// </summary>
	internal static class Enumerator
	{
		internal const string BaseUri = Endpoints.BaseUri + "/" + nameof(Enumerator);

		/// <summary>
		/// The account type route.
		/// </summary>
		internal static class AccountType
		{
			internal const string Get = nameof(AccountType);
		}

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
		/// The document types route.
		/// </summary>
		internal static class DocumentTypes
		{
			internal const string Get = nameof(DocumentTypes);
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
		internal const string GetAllLists = EmptySuffix;
		internal const string GetList = "{listId:guid}";
		internal const string CreateList = EmptySuffix;
		internal const string CreateItem = "{listId:guid}";
		internal const string UpdateList = "{listId:guid}";
		internal const string UpdateItem = "Items/{itemId:Guid}";
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

	/// <summary>
	/// The user management route.
	/// </summary>
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
